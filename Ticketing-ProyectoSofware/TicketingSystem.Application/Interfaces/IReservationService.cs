using TicketingSystem.Domain.Entities;

namespace TicketingSystem.Application.Interfaces;

public interface IReservationService
{
    Task<Reservation> CreateAsync(Guid seatId, Guid userId);
}