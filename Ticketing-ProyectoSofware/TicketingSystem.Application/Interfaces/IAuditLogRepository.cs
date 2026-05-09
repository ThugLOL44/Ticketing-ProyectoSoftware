using TicketingSystem.Domain.Entities;

namespace TicketingSystem.Application.Interfaces
{
    public interface IAuditLogRepository
    {
        Task LogAsync(AuditLog entry);
        Task LogFailureAsync(AuditLog entry);
    }
}
