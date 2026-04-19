using System;
using System.Collections.Generic;
using System.Text;
using TicketingSystem.Application.DTOs;

namespace TicketingSystem.Application.Interfaces
{
    public interface IEventService
    {
        Task<(IEnumerable<EventDto> Events, int TotalCount)> GetPagedEventsAsync(int page, int pageSize);
    }
}
