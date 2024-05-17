using OneOf;
using Error = OneOf.Types.Error;

namespace UtilityAccountManager.Services.Interfaces;

public interface IResidentUtilityAccountService
{
    Task<OneOf<int, Error>> DeleteAsync(string resUtilAccId);
}
