namespace TicketingSystem.Application.Interfaces
{
    public interface IPaymentService
    {
        Task ConfirmPaymentAsync(Guid reservationId);

    }
}
