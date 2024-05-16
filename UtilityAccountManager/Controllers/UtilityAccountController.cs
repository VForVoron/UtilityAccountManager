using Microsoft.AspNetCore.Mvc;
using UtilityAccountManager.Data.Models;
using UtilityAccountManager.Services.Interfaces;
using OneOf;
using Error = OneOf.Types.Error;

namespace UtilityAccountManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UtilityAccountController : ControllerBase
{
    private readonly IUtilityAccountService _utilityAccountService;

    public UtilityAccountController(IUtilityAccountService utilityAccountService)
    {
        _utilityAccountService = utilityAccountService;
    }

    [HttpGet]
    public async Task<ActionResult<List<UtilityAccountDto>>> GetAll([FromQuery] DateTime? startDate = null,
                                                                    [FromQuery] DateTime? endDate = null,
                                                                    [FromQuery] bool onlyWithResidents = false,
                                                                    [FromQuery] string? residentName = null,
                                                                    [FromQuery] string? address = null)
        => (await _utilityAccountService.GetAllAsync(startDate, endDate, onlyWithResidents, residentName, address)).ToList();

    [HttpPost("[action]/{UtilAccId}")]
    public async Task<ActionResult<UtilityAccountDto>> UpdateUtilAcc(string UtilAccId, UpdateUtilityAccountDto updateUtilAccDto)
    {
        var result = await _utilityAccountService.UpdateAsync(UtilAccId, updateUtilAccDto);

        if (result.IsT0)
            return result.AsT0;
        else
            return BadRequest("Utility account with the given ID doesn't exist");

    }






}
