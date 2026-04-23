using Microsoft.EntityFrameworkCore;
using TicketingSystem.Application.Interfaces;
using TicketingSystem.Domain.Entities;
using TicketingSystem.Infrastructure.Persistence;

namespace TicketingSystem.Infrastructure.Repositories;

public class SeatsRepository : ISeatsRepository
{
    private readonly AppDbContext _context;
    public SeatsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Seat>> GetByEventIdAsync(Guid eventId)
    {
        return await _context.Seats
            .Include(s => s.Sector)
            .Where(s => s.Sector.EventId == eventId)
            .OrderBy(s => s.Sector.Name)
            .ThenBy(s => s.RowIdentifier)
            .ThenBy(s => s.SeatNumber)
            .ToListAsync();
    }

    public async Task<Seat?> GetByIdAsync(Guid seatId)
    {
        return await _context.Seats
            .FirstOrDefaultAsync(s => s.Id == seatId);
    }

    public async Task UpdateAsync(Seat seat)
    {
        _context.Seats.Update(seat);
        await _context.SaveChangesAsync();
    }

}
