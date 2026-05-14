namespace TicketingSystem.Application.DTOs;

public record CreatePaymentDto(
    List<Guid> ReservationIds
);
