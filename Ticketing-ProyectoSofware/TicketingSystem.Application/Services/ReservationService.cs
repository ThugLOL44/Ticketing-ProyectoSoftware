using TicketingSystem.Application.Interfaces;
using TicketingSystem.Domain.Entities;
using TicketingSystem.Domain.Enums;
using TicketingSystem.Application.Exceptions;

namespace TicketingSystem.Application.Services;

public class ReservationService : IReservationService
{
    private readonly ISeatsRepository _seatsRepository;
    private readonly IReservationsRepository _reservationsRepository;
    private readonly IAuditLogRepository _auditLogRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ReservationService(
        ISeatsRepository seatsRepository,
        IReservationsRepository reservationsRepository,
        IAuditLogRepository auditLogRepository,
        IUnitOfWork unitOfWork)
    {
        _seatsRepository = seatsRepository;
        _reservationsRepository = reservationsRepository;
        _auditLogRepository = auditLogRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Reservation> CreateAsync(Guid seatId, Guid userId)
    {
            var seat = await _seatsRepository.GetByIdAsync(seatId)
                ?? throw new KeyNotFoundException($"Butaca {seatId} no encontrada.");

        if (seat.Status != SeatStatus.Available)
        {
            await _auditLogRepository.LogFailureAsync(new AuditLog
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Action = AuditAction.ReservationFailed,
                EntityType = "Reservation",
                EntityId = seatId.ToString(),
                Details = "Reserva fallida: la butaca no estaba disponible.",
                CreatedAt = DateTimeOffset.UtcNow
            });
            throw new InvalidOperationException("La butaca no está disponible.");
        }

        await _unitOfWork.BeginTransactionAsync();

        try
        {

            seat.Status = SeatStatus.Reserved;
            seat.Version++;
            await _seatsRepository.UpdateAsync(seat);

            var reservation = new Reservation
            {
                Id = Guid.NewGuid(),
                SeatId = seatId,
                UserId = userId,
                Status = "Pending",
                ReservedAt = DateTimeOffset.UtcNow,
                ExpiresAt = DateTimeOffset.UtcNow.AddMinutes(5)
            };

            await _reservationsRepository.CreateAsync(reservation);

            await _auditLogRepository.LogAsync(new AuditLog
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Action = AuditAction.ReservationSucceeded,
                EntityType = "Reservation",
                EntityId = reservation.Id.ToString(),
                Details = $"Butaca {seatId} reservada por usuario {userId}",
                CreatedAt = DateTimeOffset.UtcNow
            });

            await _unitOfWork.CommitAsync();
            return reservation;
        }
        catch(ConcurrencyException)
        {
            await _unitOfWork.RollbackAsync();
            _unitOfWork.ClearTracking();

            await _auditLogRepository.LogFailureAsync(new AuditLog
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Action = AuditAction.ReservationFailed,
                EntityType = "Reservation",
                EntityId = seatId.ToString(),
                Details = "Conflicto de concurrencia: otro usuario reservó la butaca simultáneamente.",
                CreatedAt = DateTimeOffset.UtcNow
            });

            throw new SeatConflictException(seatId);
        }

    }
}
