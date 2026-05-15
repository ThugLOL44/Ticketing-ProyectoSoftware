using Microsoft.AspNetCore.Mvc;
using TicketingSystem.Application.DTOs;
using TicketingSystem.Application.Exceptions;
using TicketingSystem.Application.Interfaces;

namespace TicketingSystem.WebAPI.Controllers;

[Route("api/v1/payments")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost]
    public async Task<IActionResult> ConfirmPaymentAsync([FromBody] CreatePaymentDto dto)
    {
        try
        {
            await _paymentService.ConfirmPaymentAsync(dto.ReservationIds);
            return Ok(new { Message = "Pago confirmado exitosamente." });
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { Message = ex.Message });
        }
    }

}