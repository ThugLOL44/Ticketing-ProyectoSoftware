using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TicketingSystem.Application.Exceptions;
using TicketingSystem.Application.Interfaces;

namespace TicketingSystem.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                await _transaction!.CommitAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                try{ await _transaction!.RollbackAsync(); }
                catch { }
                throw new ConcurrencyException();
            }

        }

        public async Task RollbackAsync()
        {
            try { await _transaction!.RollbackAsync(); } catch { }
        }

        public void ClearTracking() 
        {
            _context.ChangeTracker.Clear();
        }

        public async ValueTask DisposeAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();            
            }
        }
    }
}
