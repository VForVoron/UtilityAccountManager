using Microsoft.AspNetCore.Mvc;
using UtilityAccountManager.Data.Models;
using UtilityAccountManager.Services.Interfaces;
using UtilityAccountManager.Queries;

namespace UtilityAccountManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UtilityAccountController : ControllerBase
{
    private readonly IUtilityAccountService _utilityAccountService;
    private readonly IResidentUtilityAccountService _resUtilAccService;

    public UtilityAccountController(IUtilityAccountService utilityAccountService,
                                    IResidentUtilityAccountService resUtilAccService)
    {
        _utilityAccountService = utilityAccountService;
        _resUtilAccService = resUtilAccService;
    }

    [HttpGet("[action]/{utilAccId}")]
    public async Task<ActionResult<UtilityAccountDto>> Get(string utilAccId)
    {
        var result = await _utilityAccountService.GetAsync(utilAccId);

        if (result.IsT0)
            return result.AsT0;
        else
            return BadRequest("ЛС с таким ID не найден");
    }

    [HttpGet]
    public async Task<ActionResult<List<UtilityAccountDto>>> GetAll([FromQuery] UtilityAccountQuery utilAccQuery)
        => (await _utilityAccountService.GetAllAsync(utilAccQuery)).ToList();

    [HttpPost("[action]/{utilAccId}")]
    public async Task<ActionResult<UtilityAccountDto>> Update(string utilAccId, UtilityAccountUpdateDto updateUtilAccDto)
    {
        var result = await _utilityAccountService.UpdateAsync(utilAccId, updateUtilAccDto);

        if (result.IsT0)
            return result.AsT0;
        else
            return BadRequest("ЛС с таким ID не найден");
    }

    [HttpDelete("[action]/{utilAccId}")]
    public async Task<IActionResult> Delete(string utilAccId)
    {
        var result = await _utilityAccountService.DeleteAsync(utilAccId);

        if (result.IsT0)
        {
            _ = _resUtilAccService.DeleteAsync(utilAccId);
            return Ok();
        }
        else
        {
            return BadRequest("ЛС с таким ID не найден");
        }
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<UtilityAccountDto>> Add(UtilityAccountAddDto utilAccAddDto)
        => await _utilityAccountService.AddAsync(utilAccAddDto);
}
