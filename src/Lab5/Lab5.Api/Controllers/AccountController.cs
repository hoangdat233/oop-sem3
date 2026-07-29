using Lab5.Lab5.Application.DTOs;
using Lab5.Lab5.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Lab5.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request)
    {
        try
        {
            AccountDto account = await _accountService.CreateAccountAsync(request);
            return Ok(account);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{accountId}/balance")]
    public async Task<IActionResult> GetBalance(Guid accountId, [FromHeader] Guid sessionId)
    {
        try
        {
            AccountDto account = await _accountService.GetAccountAsync(accountId, sessionId);
            return Ok(account);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{accountId}/deposit")]
    public async Task<IActionResult> Deposit(Guid accountId, [FromBody] DepositWithdrawRequest request, [FromHeader] Guid sessionId)
    {
        try
        {
            AccountDto account = await _accountService.DepositAsync(accountId, request.Amount, sessionId);
            return Ok(account);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{accountId}/withdraw")]
    public async Task<IActionResult> Withdraw(Guid accountId, [FromBody] DepositWithdrawRequest request, [FromHeader] Guid sessionId)
    {
        try
        {
            AccountDto account = await _accountService.WithdrawAsync(accountId, request.Amount, sessionId);
            return Ok(account);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Insufficient funds", StringComparison.Ordinal))
        {
            return BadRequest("Insufficient funds");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{accountId}/history")]
    public async Task<IActionResult> GetOperationHistory(Guid accountId, [FromHeader] Guid sessionId)
    {
        try
        {
            IEnumerable<OperationDto> history = await _accountService.GetOperationsHistoryAsync(accountId, sessionId);
            return Ok(history);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}