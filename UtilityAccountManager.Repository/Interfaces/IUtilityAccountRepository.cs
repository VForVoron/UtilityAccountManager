
using System.Linq.Expressions;
using UtilityAccountManager.Data.Models;

namespace UtilityAccountManager.Repository.Interfaces;

public interface IUtilityAccountRepository : IRepository<UtilityAccountModel>
{
    Task<List<UtilityAccountModel>> GetAllAsync(Expression<Func<UtilityAccountModel, bool>>?[] predicates = null,
                                             Func<IQueryable<UtilityAccountModel>, IQueryable<UtilityAccountModel>>? queryFunc = null,
                                             int? take = null,
                                             params Expression<Func<UtilityAccountModel, object?>>[] includes);
}