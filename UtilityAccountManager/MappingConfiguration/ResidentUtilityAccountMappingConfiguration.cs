using Mapster;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UtilityAccountManager.Data.Models;
using System.Linq;

namespace UtilityAccountManager.MappingConfiguration;

public class ResidentUtilityAccountMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config
            .NewConfig<UtilityAccountModel, UtilityAccountDto>()
            .Map(dto => dto.Residents, utilAcc => utilAcc.ResidentUtilityAccounts.Select(rua => new ResidentModel(rua.Resident)).ToHashSet())
            .Compile();


    }
}
