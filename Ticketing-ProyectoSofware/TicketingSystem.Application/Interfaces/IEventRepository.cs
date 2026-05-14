using TicketingSystem.Application.DTOs;

namespace TicketingSystem.Application.Interfaces
{
    public interface IEventRepository
    {
        Task<(IEnumerable<EventDto> Events, int TotalCount)> GetPagedEventsAsync(int page, int pageSize);
    }
}
