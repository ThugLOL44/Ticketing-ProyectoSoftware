using System;
using System.Collections.Generic;
using System.Text;

namespace TicketingSystem.Application.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        void ClearTracking();

    }
}
