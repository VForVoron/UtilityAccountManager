using OneOf;
using UtilityAccountManager.Data.Models;
using UtilityAccountManager.Queries;
using Error = OneOf.Types.Error;

namespace UtilityAccountManager.Services.Interfaces;

public interface IUtilityAccountService
{
    Task<IReadOnlyCollection<UtilityAccountDto>> GetAllAsync(UtilityAccountQuery utilAccQuery);
    Task<OneOf<UtilityAccountDto, Error>> UpdateAsync(string UtilAccId, UtilityAccountUpdateDto updateUtilAccDto);
    Task<OneOf<UtilityAccountDto, Error>> GetAsync(string staffId);
    Task<OneOf<int, Error>> DeleteAsync(string utilAccId);
    Task<UtilityAccountDto> AddAsync(UtilityAccountAddDto utilAccAddDto);
}
