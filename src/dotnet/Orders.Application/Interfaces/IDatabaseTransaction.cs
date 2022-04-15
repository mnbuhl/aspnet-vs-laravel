namespace Orders.Application.Interfaces;

public interface IDatabaseTransaction
{
    Task BeginTransaction();
    Task CommitTransaction();
    Task RollbackTransaction();
}