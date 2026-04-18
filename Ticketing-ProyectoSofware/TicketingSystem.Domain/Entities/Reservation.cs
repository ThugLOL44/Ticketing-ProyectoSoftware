namespace TicketingSystem.Domain.Entities;


public class Reservation
{
    public Guid Id { get; set; }
    public Guid SeatId { get; set; }
    public Guid UserId { get; set; }
    public string Status { get; set; } = "Pending";
    public DateTimeOffset ReservedAt { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }

    public Seat Seat { get; set; } = null!;
    public User User { get; set; } = null!;
}