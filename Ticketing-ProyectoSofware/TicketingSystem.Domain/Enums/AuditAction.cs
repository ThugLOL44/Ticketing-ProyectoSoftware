namespace TicketingSystem.Domain.Enums;
public enum AuditAction
{
    ReservationAttempted = 0,  
    ReservationSucceeded = 1,  
    ReservationFailed = 2,  
    PaymentConfirmed = 3,  
    ReservationExpired = 4   
}