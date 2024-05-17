
using UtilityAccountManager.Data;
using UtilityAccountManager.Data.Models;
using UtilityAccountManager.Repository.Interfaces;

namespace UtilityAccountManager.Repository;

public class ResidentUtilityAccountRepository : UtilityAccountContextRepository<ResidentUtilityAccountModel>, IResidentUtilityAccountRepository
{
    public ResidentUtilityAccountRepository(UtilityAccountContext context) : base(context) { }
}