using System.Threading.Tasks;
using Orders.Application.Interfaces;

namespace Orders.Api.Test.Mocks;

public class DatabaseTransaction : IDatabaseTransaction
{
    public Task BeginTransaction()
    {
        return Task.CompletedTask;
    }

    public Task CommitTransaction()
    {
        return Task.CompletedTask;
    }

    public Task RollbackTransaction()
    {
        return Task.CompletedTask;
    }
}