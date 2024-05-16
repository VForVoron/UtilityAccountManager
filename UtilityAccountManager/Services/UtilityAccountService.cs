using System.Linq.Expressions;
using UtilityAccountManager.Data.Models;
using UtilityAccountManager.Repository.Interfaces;
using UtilityAccountManager.Services.Interfaces;
using MapsterMapper;
using UtilityAccountManager.Data;
using OneOf;
using Error = OneOf.Types.Error;

namespace UtilityAccountManager.Services;

public class UtilityAccountService : IUtilityAccountService
{
    private readonly ILogger<UtilityAccountService> _logger;
    private readonly IUtilityAccountRepository _utilityAccountRepository;
    private readonly IMapper _mapper;
    private readonly UtilityAccountContext _context;

    public UtilityAccountService(
        ILogger<UtilityAccountService> logger,
        IUtilityAccountRepository utilityAccountRepository,
        IMapper mapper,
        UtilityAccountContext context)
    {
        _logger = logger;
        _utilityAccountRepository = utilityAccountRepository;
        _mapper = mapper;
        _context = context;
    }

    public async Task<IReadOnlyCollection<UtilityAccountDto>> GetAllAsync(DateTime? startDate = null,
                                                                          DateTime? endDate = null,
                                                                          bool onlyWithResidents = false,
                                                                          string? residentName = null,
                                                                          string? address = null)
    {
        residentName = residentName?.ToLower();
        address = address?.ToLower();

        var predicates = new List<Expression<Func<UtilityAccountModel, bool>>>();

        if (startDate != null && endDate != null)
            predicates.Add(account => account.StartDate >= startDate && account.EndDate <= endDate);
        else if (startDate != null)
            predicates.Add(account => account.StartDate >= startDate);
        else if (endDate != null)
            predicates.Add(account => account.EndDate <= endDate);

        if (onlyWithResidents)
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

    public async Task<OneOf<UtilityAccountDto, Error>> UpdateAsync(string utilAccId, UpdateUtilityAccountDto updateUtilAccDto)
    {
        if (await _utilityAccountRepository.GetAsync(utilAcc => utilAcc.Id == utilAccId) is not { } utilAcc)
            return new Error();

        utilAcc = _mapper.From(updateUtilAccDto).AdaptTo(utilAcc);
        await _utilityAccountRepository.UpdateAsync(utilAcc);

        return _mapper.From(utilAcc).AdaptToType<UtilityAccountDto>();
    }

    // return await _utilityAccountRepository.GetAllAsync(account => account.AccountNumber != null);

    //return await _utilityAccountRepository.GetAllAsync(
    //    predicate: account => account.AccountNumber != null,
    //    include: account => account.ResidentUtilityAccounts);

    //return await _utilityAccountRepository.GetAllAsync(
    //    account => account.AccountNumber != null,
    //    include: new Expression<Func<UtilityAccountModel, object>>[]
    //    {
    //    account => account.ResidentUtilityAccounts,
    //    }!);

    //'account.ResidentUtilityAccounts.AsQueryable().ElementAt(0)' is invalid inside an 'Include' operation,
    //since it does not represent a property access: 't => t.MyProperty'
    // Collection navigation access can be filtered by composing  Where, OrderBy(Descending), ThenBy(Descending), Skip or Take operations

    //public async Task Delete(long id)
    //{
    //    if (await _staffRepository.GetAsync(id) is { } staff)
    //        await _staffRepository.Remove(staff);

    //    var userEmail = _userIdentityService.GetEmail()!;

    //    _logger.LogInformation(
    //        "User {User} delete staff with id {id} [{dateTime}].",
    //        userEmail, id, TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow,
    //            TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time")).ToString());
    //}


    //public async Task<StaffInfoOutputDTO?> GetAsync(long staffId)
    //{
    //    await using var scope = _serviceScopeFactory.CreateAsyncScope();
    //    var resumeRepository = scope.ServiceProvider.GetRequiredService<IReadOnlyRepository<ResumeModel>>();

    //    if (await _staffRepository.GetAsync(staffId, staff => staff.Stack,
    //            staff => staff.Contacts) is not { } staff)
    //        return null;

    //    var user = await _userService.GetAsync(staff.UserID);
    //    var users = await _userService.GetAllAsync(u => u.Role != UserRole.Remote);
    //    var result = _mapper.From(staff).AdaptToType<StaffInfoOutputDTO>();
    //    if (staff.CvId is { } cvId
    //        && await resumeRepository.GetAsync(cvId) is { } resume)
    //        result.LastDownloadResume = resume.LastDownloadResume;
    //    result.User = _mapper.From(user).AdaptToType<OutputUserDTO>();
    //    result.UsersToAssignAsResponsible = _mapper.From(users).AdaptToType<List<OutputUserDTO>>();

    //    return result;
    //}
}
