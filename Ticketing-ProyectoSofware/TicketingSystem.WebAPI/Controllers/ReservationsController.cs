using Microsoft.AspNetCore.Mvc;
using TicketingSystem.Application.DTOs;
using TicketingSystem.Application.Exceptions;
using TicketingSystem.Application.Interfaces;


namespace TicketingSystem.WebAPI.Controllers
{

    [ApiController]
    [Route("api/v1/reservations")]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationDto request)
        {
            try
            {
                var reservation = await _reservationService.CreateAsync(request.SeatId, request.UserId);

                var dto = new ReservationDto
                {
                    Id = reservation.Id,
                    SeatId = reservation.SeatId,
                    UserId = reservation.UserId,
                    Status = reservation.Status,
                    ReservedAt = reservation.ReservedAt,
                    ExpiresAt = reservation.ExpiresAt
                };

                return Created(string.Empty, dto);
            }
            catch (SeatConflictException ex)
            {
                return Conflict (new { error = ex.Message, seatId = ex.SeatId });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { error = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpPost("cancel")]
        public async Task<IActionResult> CancelReservations([FromBody] CancelReservationsDto request)
        {

            await _reservationService.CancelReservationsAsync(request.ReservationIds ?? new List<Guid>());
            return NoContent();
            
        }
    }
}