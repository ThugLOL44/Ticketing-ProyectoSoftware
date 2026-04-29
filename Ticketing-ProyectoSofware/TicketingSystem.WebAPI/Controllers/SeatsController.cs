using Microsoft.AspNetCore.Mvc;
using TicketingSystem.Application.UseCases;
using TicketingSystem.WebAPI.DTOs;

namespace TicketingSystem.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/events/{eventId}/seats")]
    public class SeatsController : ControllerBase
    {
        private readonly GetSeatsByEventUseCase _getSeatsByEvent;

        public SeatsController(GetSeatsByEventUseCase getSeatsByEvent)
        {
            _getSeatsByEvent = getSeatsByEvent;
        }

        [HttpGet]
        public async Task<IActionResult> GetSeats(Guid eventId)
        {
            var seats = await _getSeatsByEvent.ExecuteAsync(eventId);

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