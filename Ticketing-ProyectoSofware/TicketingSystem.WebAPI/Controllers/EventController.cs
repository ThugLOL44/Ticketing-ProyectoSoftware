using Azure;
using Microsoft.AspNetCore.Mvc;
using TicketingSystem.Application.Interfaces;

namespace TicketingSystem.WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPagedEvents(int page = 1, int pageSize = 10)
        {
            var (events, totalCount) = await _eventService.GetPagedEventsAsync(page, pageSize);
            return Ok(new { Events = events, TotalCount = totalCount, Page = page, PageSize = pageSize, TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)});
        }
    }
}
