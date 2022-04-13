using Orders.Domain.Models;

namespace Orders.Application.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<T?> Get(Guid id);
    Task<T?> GetWithSpecification();
    Task<IReadOnlyList<T>> List();
    Task<IReadOnlyList<T>> ListWithSpecification();
    Task<bool> Create(T entity);
    Task<bool> Update(T entity);
    Task<bool> Delete(Guid id);
}