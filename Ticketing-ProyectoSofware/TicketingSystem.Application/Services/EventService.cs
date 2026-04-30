using TicketingSystem.Application.DTOs;
using TicketingSystem.Application.Interfaces;

namespace TicketingSystem.Application.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<(IEnumerable<EventDto> Events, int TotalCount)> GetPagedEventsAsync(int page, int pageSize)
        {
            if(page < 1) page = 1;
            if (pageSize < 1 || pageSize > 50) pageSize = 10;

            return await _eventRepository.GetPagedEventsAsync(page, pageSize);
        }
    }
}
