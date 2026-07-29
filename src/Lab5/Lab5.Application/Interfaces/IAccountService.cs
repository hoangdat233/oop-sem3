using Lab5.Lab5.Application.DTOs;

namespace Lab5.Lab5.Application.Interfaces;

public interface IAccountService
{
    Task<AccountDto> CreateAccountAsync(CreateAccountRequest request);

    Task<AccountDto> GetAccountAsync(Guid accountId, Guid sessionId);

    Task<AccountDto> DepositAsync(Guid accountId, decimal amount, Guid sessionId);

    Task<AccountDto> WithdrawAsync(Guid accountId, decimal amount, Guid sessionId);

    Task<IEnumerable<OperationDto>> GetOperationsHistoryAsync(Guid accountId, Guid sessionId);
}