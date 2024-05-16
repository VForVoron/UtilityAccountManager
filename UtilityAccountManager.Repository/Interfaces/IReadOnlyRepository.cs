using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UtilityAccountManager.Repository.Interfaces;

public interface IReadOnlyRepository<T> where T : class
{
    Task<bool> ContainsAsync(Expression<Func<T, bool>> predicat);
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object?>>[] include);
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>>[]? predicates = null,
                              Func<IQueryable<T>, IQueryable<T>>? queryFunc = null,
                              int? take = null,
                              params Expression<Func<T, object?>>[] includes);
}
