namespace TicketingSystem.Application.DTOs;
public class EventDto
{
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Venue { get; set; } = string.Empty;
        public DateTimeOffset EventDate { get; set; }
        public string status { get; set; } = string.Empty;
}

