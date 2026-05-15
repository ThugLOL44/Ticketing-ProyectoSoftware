namespace TicketingSystem.Application.Exceptions
{
    public class SeatConflictException : Exception
    {
        public Guid SeatId { get; }

        public SeatConflictException(Guid seatId) :base($"El asiento {seatId} fue tomada por otro usuario simultáneamente.")
        {
            SeatId = seatId;
        }
    }
}
