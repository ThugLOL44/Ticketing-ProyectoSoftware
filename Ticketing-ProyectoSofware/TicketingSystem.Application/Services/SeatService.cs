using TicketingSystem.Application.Interfaces;
using TicketingSystem.Domain.Entities;

namespace TicketingSystem.Application.Services;

public class SeatService : ISeatService
{
    private readonly ISeatsRepository _seatsRepository;

    public SeatService(ISeatsRepository seatsRepository)
    {
        _seatsRepository = seatsRepository;
    }

    public async Task<IEnumerable<Seat>> GetByEventIdAsync(Guid eventId)
    {
        return await _seatsRepository.GetByEventIdAsync(eventId);
    }
}