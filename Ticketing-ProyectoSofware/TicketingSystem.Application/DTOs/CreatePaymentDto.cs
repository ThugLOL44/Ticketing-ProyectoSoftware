using System;
using System.Collections.Generic;
using System.Text;

namespace TicketingSystem.Application.DTOs
{
    public class CreatePaymentDto
    {
        public Guid ReservationId { get; set; }
    }
}
