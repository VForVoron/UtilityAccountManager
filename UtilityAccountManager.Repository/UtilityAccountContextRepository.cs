using UtilityAccountManager.Data;

namespace UtilityAccountManager.Repository;

public class UtilityAccountContextRepository<T> : Repository<T, UtilityAccountContext> where T : class
{
    public UtilityAccountContextRepository(UtilityAccountContext context) : base(context) { }
}
