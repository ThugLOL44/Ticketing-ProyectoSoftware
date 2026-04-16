namespace TicketingSystem.Domain.Enums;

/// <summary>
/// Categoriza cada evento registrado en la tabla AuditLog.
/// Permite trazabilidad completa del ciclo de vida de una reserva.
/// EF Core persiste estos valores como string en la BD.
/// </summary>
/// 
public enum AuditAction
{
    ReservationAttempted = 0,  // Todo intento de reserva, antes de saber si tuvo éxito
    ReservationSucceeded = 1,  // Reserva confirmada correctamente
    ReservationFailed = 2,  // Falló, por ejemplo butaca ya ocupada
    PaymentConfirmed = 3,  // Pago procesado, butaca pasa a Sold (Entrega 2)
    ReservationExpired = 4   // Background job liberó la butaca por timeout (Entrega 2)
}