using TicketingSystem.Domain.Entities;

namespace TicketingSystem.Application.Interfaces;

public interface IReservationsRepository

{
    Task<Reservation> CreateAsync(Reservation reservation);
    Task<Reservation?> GetByIdWithSeatAsync(Guid reservationId);
}