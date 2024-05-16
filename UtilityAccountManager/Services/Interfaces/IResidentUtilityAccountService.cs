using OneOf;
using Error = OneOf.Types.Error;

namespace UtilityAccountManager.Services.Interfaces;

public interface IResidentUtilityAccountService
{
    //Task<IReadOnlyCollection<UtilityAccountDto>> GetAllAsync(UtilityAccountQuery utilAccQuery);
    //Task<OneOf<UtilityAccountDto, Error>> UpdateAsync(string UtilAccId, UtilityAccountUpdateDto updateUtilAccDto);
    //Task<OneOf<UtilityAccountDto, Error>> GetAsync(string staffId);
    Task<OneOf<int, Error>> DeleteAsync(string resUtilAccId);
    // Task<UtilityAccountDto> AddAsync(UtilityAccountAddDto utilAccAddDto);
}
