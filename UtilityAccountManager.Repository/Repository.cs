using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UtilityAccountManager.Repository.Interfaces;

namespace UtilityAccountManager.Repository;

public class Repository<T, TContext> : IRepository<T>
    where T : class
    where TContext : DbContext
{
    protected readonly TContext _context;

    public Repository(TContext dbContext)
    {
        _context = dbContext;
    }

    public virtual async Task AddAsync(T entity)
    {
        _ = await _context.Set<T>().AddAsync(entity);
        _ = await _context.SaveChangesAsync();
    }

    public virtual Task<List<T>> GetAllAsync(Expression<Func<T, bool>>[]? predicates = null,
                                             Func<IQueryable<T>, IQueryable<T>>? queryFunc = null,
                                             int? take = null,
                                             params Expression<Func<T, object?>>[] includes)
    {
        IQueryable<T> query = _context.Set<T>();

        if (includes != null)
            query = includes.Aggregate(query, (currentQuery, include) => currentQuery.Include(include));

        if (predicates != null)
            query = predicates.Aggregate(query, (currentQuery, predicate) => currentQuery.Where(predicate));

        if (queryFunc != null)
            query = queryFunc(query);

        if (take != null)
            query = query.Take(take.Value);

        return query.ToListAsync();
    }

    public virtual Task<T?> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object?>>[] include)
    {
        IQueryable<T> query = _context.Set<T>();

        query = include.Aggregate(query, (currentQuery, include) => currentQuery.Include(include));

        return query.SingleOrDefaultAsync(predicate);
    }

    public virtual Task<int> DeleteAsync(T entity)
    {
        _ = _context.Set<T>().Remove(entity);
        return _context.SaveChangesAsync();
    }

    public virtual Task UpdateAsync(T entity)
    {
        _ = _context.Set<T>().Update(entity);
        return _context.SaveChangesAsync();
    }

    public Task<bool> ContainsAsync(Expression<Func<T, bool>> predicat)
      => _context
       .Set<T>()
       .AsNoTracking()
       .IgnoreAutoIncludes()
       .AnyAsync(predicat);
}