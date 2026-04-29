namespace TicketingSystem.WebAPI.DTOs;

public class ReservationDto
{
    public Guid Id { get; set; }
    public Guid SeatId { get; set; }
    public Guid UserId { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTimeOffset ReservedAt { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }
}