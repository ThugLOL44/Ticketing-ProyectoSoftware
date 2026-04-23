using TicketingSystem.Application.Interfaces;
using TicketingSystem.Domain.Entities;
using TicketingSystem.Domain.Enums;

namespace TicketingSystem.Application.UseCases;

public class CreateReservationUseCase
{
    private readonly ISeatsRepository _seatsRepository;
    private readonly IReservationsRepository _reservationsRepository;
    private readonly IAuditLogRepository _auditLogRepository;

    public CreateReservationUseCase(
        ISeatsRepository seatsRepository,
        IReservationsRepository reservationsRepository,
        IAuditLogRepository auditLogRepository)
    {
        _seatsRepository = seatsRepository;
        _reservationsRepository = reservationsRepository;
        _auditLogRepository = auditLogRepository;
    }

    public async Task<Reservation> ExecuteAsync(Guid seatId, Guid userId)
    {
        var seat = await _seatsRepository.GetByIdAsync(seatId)
            ?? throw new KeyNotFoundException($"Butaca {seatId} no encontrada.");

        if (seat.Status != SeatStatus.Available)
            throw new InvalidOperationException("La butaca no está disponible.");

        seat.Status = SeatStatus.Reserved;
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

        return reservation;
    }
}