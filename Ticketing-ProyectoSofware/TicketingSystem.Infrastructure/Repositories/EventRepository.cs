using Microsoft.EntityFrameworkCore;
using TicketingSystem.Application.DTOs;
using TicketingSystem.Application.Interfaces;
using TicketingSystem.Infrastructure.Persistence;

namespace TicketingSystem.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _context;

        public EventRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<EventDto> Events, int TotalCount)> GetPagedEventsAsync(int page, int pageSize)
        {
            var query = _context.Events
                .Where(e => e.Status == "Active")
                .OrderBy(e => e.EventDate);

            var totalCount = await query.CountAsync();

            var events = await query
                .Skip((page - 1) * pageSize)    
                .Take(pageSize)
                .Select(e => new EventDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Venue = e.Venue,
                    EventDate = e.EventDate,
                    Status = e.Status,
                    ImageUrl = e.ImageUrl
                })
                .ToListAsync();

            return (events, totalCount);
        }

    }
}
