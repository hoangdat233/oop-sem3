using Lab5.Lab5.Application.DTOs;
using Lab5.Lab5.Application.Interfaces;
using Lab5.Lab5.Domain.Entities;
using Lab5.Lab5.Domain.Exceptions;
using Lab5.Lab5.Domain.Interfaces;

namespace Lab5.Lab5.Application.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IOperationRepository _operationRepository;
    private readonly ISessionService _sessionService;

    public AccountService(
        IAccountRepository accountRepository,
        IOperationRepository operationRepository,
        ISessionService sessionService)
    {
        _accountRepository = accountRepository;
        _operationRepository = operationRepository;
        _sessionService = sessionService;
    }

    public async Task<AccountDto> CreateAccountAsync(CreateAccountRequest request)
    {
        if (await _accountRepository.ExistsAsync(request.AccountNumber))
            throw new ArgumentException("Account with this number already exists");

        var account = new Account(
            request.AccountNumber,
            request.PinCode,
            0);
        await _accountRepository.AddAsync(account);

        await LogOperationAsync(
            account.Id,
            OperationType.AccountCreation,
            0,
            account.Balance,
            $"Account created with initial balance: 0");

        return MapToDto(account);
    }

    public async Task<AccountDto> GetAccountAsync(Guid accountId, Guid sessionId)
    {
        await ValidateUserAccessAsync(accountId, sessionId);

        Account account = await GetAccountByIdAsync(accountId);

        await LogOperationAsync(
            accountId,
            OperationType.BalanceCheck,
            0,
            account.Balance,
            "Balance checked");

        return MapToDto(account);
    }

    public async Task<AccountDto> DepositAsync(Guid accountId, decimal amount, Guid sessionId)
    {
        await ValidateUserAccessAsync(accountId, sessionId);

        if (amount <= 0)
            throw new ArgumentException("Amount must be positive");

        Account account = await GetAccountByIdAsync(accountId);
        account.Deposit(amount);
        await _accountRepository.UpdateAsync(account);

        await LogOperationAsync(
            accountId,
            OperationType.Deposit,
            amount,
            account.Balance,
            $"Deposit: +{amount}");

        return MapToDto(account);
    }

    public async Task<AccountDto> WithdrawAsync(Guid accountId, decimal amount, Guid sessionId)
    {
        await ValidateUserAccessAsync(accountId, sessionId);

        if (amount <= 0)
            throw new ArgumentException("Amount must be positive");

        Account account = await GetAccountByIdAsync(accountId);
        account.Withdraw(amount);
        await _accountRepository.UpdateAsync(account);

        await LogOperationAsync(
            accountId,
            OperationType.Withdrawal,
            amount,
            account.Balance,
            $"Withdrawal: -{amount}");

        return MapToDto(account);
    }

    public async Task<IEnumerable<OperationDto>> GetOperationsHistoryAsync(Guid accountId, Guid sessionId)
    {
        await ValidateUserAccessAsync(accountId, sessionId);

        IEnumerable<Operation> operations = await _operationRepository.GetByAccountIdAsync(accountId);
        return operations.Select(MapToDto);
    }

    private async Task ValidateUserAccessAsync(Guid accountId, Guid sessionId)
    {
        bool isValid = await _sessionService.ValidateSessionAsync(sessionId);
        if (!isValid)
            throw new InvalidSessionException("Invalid or expired session");

        Guid? sessionAccountId = await _sessionService.GetAccountIdFromSessionAsync(sessionId);
        if (sessionAccountId != accountId && !await _sessionService.ValidateSessionAsync(sessionId, requireAdmin: true))
            throw new UnauthorizedAccessException("Access denied");
    }

    private async Task<Account> GetAccountByIdAsync(Guid accountId)
    {
        Account? account = await _accountRepository.GetByIdAsync(accountId);
        return account ?? throw new AccountNotFoundException($"Account with ID {accountId} not found");
    }

    private async Task LogOperationAsync(
        Guid accountId,
        OperationType type,
        decimal amount,
        decimal balanceAfter,
        string description)
    {
        var operation = new Operation(accountId, type, amount, balanceAfter, description);
        await _operationRepository.AddAsync(operation);
    }

    private AccountDto MapToDto(Account account) => new()
    {
        Id = account.Id,
        AccountNumber = account.AccountNumber,
        Balance = account.Balance,
        CreatedAt = account.CreatedAt,
    };

    private OperationDto MapToDto(Operation operation) => new()
    {
        Id = operation.Id,
        Type = operation.Type,
        Amount = operation.Amount,
        BalanceAfter = operation.BalanceAfter,
        Timestamp = operation.Timestamp,
        Description = operation.Description,
    };
}