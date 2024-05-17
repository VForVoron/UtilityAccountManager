namespace UtilityAccountManager.Repository.Interfaces;

public interface IRepository<T> : IReadOnlyRepository<T> where T : class
{
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task<int> DeleteAsync(T entity);
}