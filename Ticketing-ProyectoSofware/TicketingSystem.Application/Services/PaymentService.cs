using TicketingSystem.Application.Interfaces;

namespace TicketingSystem.Application.Services
{
    public class PaymentService : IPaymentService
    {
        public Task ConfirmPaymentAsync(Guid reservationId)
        {
            throw new NotImplementedException();
        }
    }
}
