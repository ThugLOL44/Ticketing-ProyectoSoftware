namespace TicketingSystem.Domain.Enums;

/// <summary>
/// Define los estados posibles de una butaca.
/// EF Core persiste estos valores como string en la BD ("Available", "Reserved", "Sold")
/// para respetar el diagrama del TP, manteniendo type safety en el código.
/// </summary>
/// 
public enum SeatStatus
{
    Available = 0,
    Reserved = 1,
    Sold = 2
}