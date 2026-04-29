using TicketingSystem.Domain.Entities;

namespace TicketingSystem.Application.Interfaces;
public interface ISeatsRepository

{
    Task<IEnumerable<Seat>> GetByEventIdAsync(Guid eventId);
    Task<Seat?> GetByIdAsync(Guid seatId);
    Task UpdateAsync(Seat seat);
}