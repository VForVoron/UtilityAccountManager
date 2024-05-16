using OneOf;
using UtilityAccountManager.Data.Models;
using OneOf;
using Error = OneOf.Types.Error;

namespace UtilityAccountManager.Services.Interfaces;

public interface IUtilityAccountService
{
    Task<IReadOnlyCollection<UtilityAccountDto>> GetAllAsync(DateTime? startDate = null,
                                                             DateTime? endDate = null,
                                                             bool onlyWithResidents = false,
                                                             string? residentName = null,
                                                             string? address = null);

    Task<OneOf<UtilityAccountDto, Error>> UpdateAsync(string UtilAccId, UpdateUtilityAccountDto updateUtilAccDto);





    //Task Delete(long id);
    //Task<StaffInfoOutputDTO?> GetAsync(long staffId);
}
