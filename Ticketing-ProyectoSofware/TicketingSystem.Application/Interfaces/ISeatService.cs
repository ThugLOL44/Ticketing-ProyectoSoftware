using TicketingSystem.Domain.Entities;

namespace TicketingSystem.Application.Interfaces;

public interface ISeatService
{
    Task<IEnumerable<Seat>> GetByEventIdAsync(Guid eventId);
}