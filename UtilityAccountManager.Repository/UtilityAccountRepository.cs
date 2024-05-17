using System.Linq.Expressions;
using UtilityAccountManager.Data;
using UtilityAccountManager.Data.Models;
using Microsoft.EntityFrameworkCore;
using UtilityAccountManager.Repository.Interfaces;

namespace UtilityAccountManager.Repository;

public class UtilityAccountRepository : UtilityAccountContextRepository<UtilityAccountModel>, IUtilityAccountRepository
{
    public UtilityAccountRepository(UtilityAccountContext context) : base(context) { }

    public override Task<List<UtilityAccountModel>> GetAllAsync(Expression<Func<UtilityAccountModel, bool>>?[] predicates = null,
                                         Func<IQueryable<UtilityAccountModel>, IQueryable<UtilityAccountModel>>? queryFunc = null,
                                         int? take = null,
                                         params Expression<Func<UtilityAccountModel, object?>>[] includes)
    {
        IQueryable<UtilityAccountModel> query = _context.Set<UtilityAccountModel>();

        query = query
            .Include(utilAcc => utilAcc.Address)
            .Include(utilAcc => utilAcc.ResidentUtilityAccounts)
                .ThenInclude(rua => rua.Resident);

        if (predicates != null)
            query = predicates.Aggregate(query, (currentQuery, predicate) => currentQuery.Where(predicate));

        if (queryFunc != null)
            query = queryFunc(query);

        if (take is not null)
            query = query.Take(take.Value);

        return query.ToListAsync();
    }
}