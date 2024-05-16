using UtilityAccountManager.Repository.Interfaces;
using UtilityAccountManager.Services.Interfaces;
using MapsterMapper;
using UtilityAccountManager.Data;
using OneOf;
using Error = OneOf.Types.Error;

namespace UtilityAccountManager.Services;

public class ResidentUtilityAccountService : IResidentUtilityAccountService
{
    private readonly ILogger<UtilityAccountService> _logger;
    private readonly IResidentUtilityAccountRepository _resUtilAccRepository;
    private readonly IMapper _mapper;
    private readonly UtilityAccountContext _context;

    public ResidentUtilityAccountService(
        ILogger<UtilityAccountService> logger,
        IResidentUtilityAccountRepository resUtilAccRepository,
        IMapper mapper,
        UtilityAccountContext context)
    {
        _logger = logger;
        _resUtilAccRepository = resUtilAccRepository;
        _mapper = mapper;
        _context = context;
    }

    public async Task<OneOf<int, Error>> DeleteAsync(string resUtilAccId)
    {
        bool isNumeric = int.TryParse(resUtilAccId, out int resUtilAccNumberId);

        var resUtilAcc = await _resUtilAccRepository
            .GetAsync(rua => isNumeric
                ? rua.ResidentId == resUtilAccNumberId
                : rua.UtilityAccountNumber == resUtilAccId);

        if (resUtilAcc == null)
            return new Error();

        return await _resUtilAccRepository.DeleteAsync(resUtilAcc);
    }
}
