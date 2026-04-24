using Microsoft.AspNetCore.Mvc;
using TicketingSystem.Application.UseCases;
using TicketingSystem.WebAPI.DTOs;

namespace TicketingSystem.WebAPI.Controllers
{

    [ApiController]
    [Route("api/v1/reservations")]
    public class ReservationsController : ControllerBase
    {
        private readonly CreateReservationUseCase _createReservation;

        public ReservationsController(CreateReservationUseCase createReservation)
        {
            _createReservation = createReservation;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationDto request)
        {
            try
            {
                var reservation = await _createReservation.ExecuteAsync(request.SeatId, request.UserId);
                return CreatedAtAction(nameof(CreateReservation), new { id = reservation.Id }, reservation);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }
    }
}