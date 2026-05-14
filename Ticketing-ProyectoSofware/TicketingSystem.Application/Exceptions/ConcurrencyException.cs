namespace TicketingSystem.Application.Exceptions
{
    public class ConcurrencyException : Exception
    { 
        public ConcurrencyException() : base("Ocurrió un conflicto de concurrencia al guardar los cambios.")
        {
        }
    }
}
