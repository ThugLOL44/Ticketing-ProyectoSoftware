namespace TicketingSystem.Application.DTOs;

public class CreateReservationDto
{
        public Guid SeatId { get; set; }
        public Guid UserId { get; set; }
}
