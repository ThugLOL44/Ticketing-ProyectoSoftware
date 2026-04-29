namespace TicketingSystem.WebAPI.DTOs;

public class SeatDto
{
    public Guid Id { get; set; }
    public string RowIdentifier { get; set; } = string.Empty;
    public int SeatNumber { get; set; }
    public string Status { get; set; } = string.Empty;
    public Guid SectorId { get; set; }
    public string SectorName { get; set; } = string.Empty;
    public decimal SectorPrice { get; set; }
}