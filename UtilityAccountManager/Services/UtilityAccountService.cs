using System.Linq.Expressions;
using UtilityAccountManager.Data.Models;
using UtilityAccountManager.Repository.Interfaces;
using UtilityAccountManager.Services.Interfaces;
using MapsterMapper;
using OneOf;
using Error = OneOf.Types.Error;
using UtilityAccountManager.Queries;

namespace UtilityAccountManager.Services;

public class UtilityAccountService : IUtilityAccountService
{
    private readonly ILogger<UtilityAccountService> _logger;
    private readonly IUtilityAccountRepository _utilityAccountRepository;
    private readonly IMapper _mapper;

    public UtilityAccountService(
        ILogger<UtilityAccountService> logger,
        IUtilityAccountRepository utilityAccountRepository,
        IMapper mapper)
    {
        _logger = logger;
        _utilityAccountRepository = utilityAccountRepository;
        _mapper = mapper;
    }

    public async Task<OneOf<UtilityAccountDto, Error>> GetAsync(string utilAccId)
    {
        if (await _utilityAccountRepository.GetAsync(utilAcc => utilAcc.Id == utilAccId) is not { } utilAcc)
            return new Error();

        return _mapper.From(utilAcc).AdaptToType<UtilityAccountDto>();
    }

    public async Task<IReadOnlyCollection<UtilityAccountDto>> GetAllAsync(UtilityAccountQuery utilAccQuery)
    {
        string? residentName = utilAccQuery?.ResidentName?.ToLower();
        string? address = utilAccQuery?.Address?.ToLower();

        var predicates = new List<Expression<Func<UtilityAccountModel, bool>>>();

        if (utilAccQuery?.StartDate != null && utilAccQuery?.EndDate != null)
            predicates.Add(account => account.StartDate >= utilAccQuery.StartDate && account.EndDate <= utilAccQuery.EndDate);
        else if (utilAccQuery?.StartDate != null)
            predicates.Add(account => account.StartDate >= utilAccQuery.StartDate);
        else if (utilAccQuery?.EndDate != null)
            predicates.Add(account => account.EndDate <= utilAccQuery.EndDate);

        if (utilAccQuery!.OnlyWithResidents)
            predicates.Add(account => account.ResidentUtilityAccounts.Any());

        //if (residentName != null)
        //    predicates.Add(account => account.ResidentUtilityAccounts.Any(rua => rua.Resident.FullName.Contains(residentName)));

        //if (address != null)
        //    predicates.Add(account => account.Address.FullAddress.Contains(address));

        var accounts = await _utilityAccountRepository.GetAllAsync(predicates.ToArray());

        if (residentName != null)
            accounts = accounts.Where(account => account.ResidentUtilityAccounts.Any(rua => rua.Resident.FullName.ToLower().Contains(residentName))).ToList();

        if (address != null)
            accounts = accounts.Where(account => account.Address.FullAddress.ToLower().Contains(address)).ToList();

        return _mapper.From(accounts).AdaptToType<List<UtilityAccountDto>>();
    }

    public async Task<OneOf<UtilityAccountDto, Error>> UpdateAsync(string utilAccId, UtilityAccountUpdateDto updateUtilAccDto)
    {
        if (await _utilityAccountRepository.GetAsync(utilAcc => utilAcc.Id == utilAccId) is not { } utilAcc)
            return new Error();

        utilAcc = _mapper.From(updateUtilAccDto).AdaptTo(utilAcc);
        await _utilityAccountRepository.UpdateAsync(utilAcc);

        return _mapper.From(utilAcc).AdaptToType<UtilityAccountDto>();
    }

    public async Task<OneOf<int, Error>> DeleteAsync(string utilAccId)
    {
        if (await _utilityAccountRepository.GetAsync(utilAcc => utilAcc.Id == utilAccId) is not { } utilAcc)
            return new Error();

        return await _utilityAccountRepository.DeleteAsync(utilAcc);
    }

    public async Task<UtilityAccountDto> AddAsync(UtilityAccountAddDto utilAccAddDto)
    {
        var utilAcc = _mapper.From(utilAccAddDto).AdaptToType<UtilityAccountModel>();
        await _utilityAccountRepository.AddAsync(utilAcc);

        return _mapper.From(utilAcc).AdaptToType<UtilityAccountDto>();
    }
}
