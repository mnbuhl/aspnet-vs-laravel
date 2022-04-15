using Orders.Application.Interfaces;

namespace Orders.Infrastructure.Data;

public class DatabaseTransaction : IDatabaseTransaction
{
    private readonly AppDbContext _context;

    public DatabaseTransaction(AppDbContext context)
    {
        _context = context;
    }

    public async Task BeginTransaction()
    {
        await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransaction()
    {
        await _context.Database.CommitTransactionAsync();
    }

    public async Task RollbackTransaction()
    {
        await _context.Database.RollbackTransactionAsync();
    }
}