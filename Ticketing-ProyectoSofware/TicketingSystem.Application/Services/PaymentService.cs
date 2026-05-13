using TicketingSystem.Application.Exceptions;
using TicketingSystem.Application.Interfaces;
using TicketingSystem.Domain.Entities;
using TicketingSystem.Domain.Enums;

namespace TicketingSystem.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly IReservationsRepository _reservationsRepository;
    private readonly IAuditLogRepository _auditLogRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PaymentService(
        IReservationsRepository reservationsRepository,
        IAuditLogRepository auditLogRepository,
        IUnitOfWork unitOfWork)
    {
        _reservationsRepository = reservationsRepository;
        _auditLogRepository = auditLogRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task ConfirmPaymentAsync(Guid reservationId)
    {
        var reservation = await _reservationsRepository.GetByIdWithSeatAsync(reservationId)
            ?? throw new NotFoundException("No se encontró la reserva.");

        if (reservation.Status != "Pending")
            throw new InvalidOperationException("La reserva no está disponible para pagar.");

        await Task.Delay(1500);

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            reservation.Status = "Paid";
            reservation.Seat.Status = SeatStatus.Sold;

            await _auditLogRepository.LogAsync(BuildAuditLog(reservation));

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    private static AuditLog BuildAuditLog(Reservation reservation) => new()
    {
        Id = Guid.NewGuid(),
        Action = AuditAction.PaymentConfirmed,
        EntityType = "Reservation",
        EntityId = reservation.Id.ToString(),
        Details = $"Pago confirmado. Butaca {reservation.SeatId} vendida.",
        CreatedAt = DateTimeOffset.UtcNow
    };
}