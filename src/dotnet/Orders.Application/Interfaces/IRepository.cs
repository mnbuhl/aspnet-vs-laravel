using Orders.Domain.Models;

namespace Orders.Application.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<T?> Get(Guid id);
    Task<T?> GetWithSpecification(ISpecification<T> spec);
    Task<IReadOnlyList<T>> List();
    Task<IReadOnlyList<T>> ListWithSpecification(ISpecification<T> spec);
    Task<bool> Create(T entity);
    Task<bool> Update(T entity);
    Task<bool> Delete(Guid id);
    Task<int> Count(ISpecification<T> spec);
}