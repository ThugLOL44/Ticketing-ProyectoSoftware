namespace TicketingSystem.Domain.Entities;

public class Event
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Venue { get; set; } = string.Empty;
    public DateTimeOffset EventDate { get; set; }
    public string Status { get; set; } = "Active";

    public ICollection<Sector> Sectors { get; set; } = new List<Sector>();
}