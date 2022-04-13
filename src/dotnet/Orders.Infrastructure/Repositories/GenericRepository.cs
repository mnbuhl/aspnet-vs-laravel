using Microsoft.EntityFrameworkCore;
using Orders.Application.Interfaces;
using Orders.Domain.Models;
using Orders.Infrastructure.Data;

namespace Orders.Infrastructure.Repositories;

public class GenericRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _context;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<T?> Get(Guid id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public Task<T?> GetWithSpecification()
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<T>> List()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public Task<IReadOnlyList<T>> ListWithSpecification()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Create(T entity)
    {
        _context.Set<T>().Add(entity);
        
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Detached;
        _context.Set<T>().Update(entity);

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(Guid id)
    {
        var entity = await _context.Set<T>().FindAsync(id);

        if (entity == null)
            return false;

        _context.Set<T>().Remove(entity);

        return await _context.SaveChangesAsync() > 0;
    }
}