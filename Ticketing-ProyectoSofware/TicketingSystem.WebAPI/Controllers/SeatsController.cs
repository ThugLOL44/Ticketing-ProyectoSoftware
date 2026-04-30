using Microsoft.AspNetCore.Mvc;
using TicketingSystem.Application.DTOs;
using TicketingSystem.Application.Interfaces;

namespace TicketingSystem.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/events/{eventId}/seats")]
    public class SeatsController : ControllerBase
    {
        private readonly ISeatService _seatService;

        public SeatsController(ISeatService seatService)
        {
            _seatService = seatService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSeats(Guid eventId)
        {
            var seats = await _seatService.GetByEventIdAsync(eventId);

            var result = seats.Select(s => new SeatDto
            {
                Id = s.Id,
                RowIdentifier = s.RowIdentifier,
                SeatNumber = s.SeatNumber,
                Status = s.Status.ToString(),
                SectorId = s.SectorId,
                SectorName = s.Sector.Name,
                SectorPrice = s.Sector.Price
            });

            return Ok(result);
        }
    }
}