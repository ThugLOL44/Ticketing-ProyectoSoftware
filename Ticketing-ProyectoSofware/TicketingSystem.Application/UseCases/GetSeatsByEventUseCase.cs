using TicketingSystem.Application.Interfaces;
using TicketingSystem.Domain.Entities;

namespace TicketingSystem.Application.UseCases
{
    public class GetSeatsByEventUseCase
    {
        private readonly ISeatsRepository _seatsRepository;

        public GetSeatsByEventUseCase(ISeatsRepository seatsRepository)
        {
            _seatsRepository = seatsRepository;

        }
        public async Task<IEnumerable<Seat>> ExecuteAsync(Guid eventId)
        {
            return await _seatsRepository.GetByEventIdAsync(eventId);
        }
    }
}