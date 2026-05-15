namespace TicketingSystem.Application.Interfaces;

public interface IPaymentService
{
    Task ConfirmPaymentAsync(List<Guid> reservationIds);
}