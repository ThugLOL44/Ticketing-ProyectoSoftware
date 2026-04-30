using TicketingSystem.Domain.Enums;

namespace TicketingSystem.Domain.Entities;

public class Seat
{
    public Guid Id { get; set; }
    public Guid SectorId { get; set; }
    public string RowIdentifier { get; set; } = string.Empty;
    public int SeatNumber { get; set; }
    public SeatStatus Status { get; set; } = SeatStatus.Available;
    public int Version { get; set; }

    public Sector Sector { get; set; } = null!;
    public Reservation? ActiveReservation { get; set; }
}