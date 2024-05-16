using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityAccountManager.Repository.Interfaces;

public interface IRepository<T> : IReadOnlyRepository<T> where T : class
{
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task<int> DeleteAsync(T entity);
}