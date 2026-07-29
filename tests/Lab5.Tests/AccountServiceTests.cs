using Lab5.Lab5.Application.DTOs;
using Lab5.Lab5.Application.Interfaces;
using Lab5.Lab5.Application.Services;
using Lab5.Lab5.Domain.Entities;
using Lab5.Lab5.Domain.Interfaces;
using NSubstitute;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab5.Tests;

public class AccountServiceTests
{
    private readonly IAccountRepository _accountRepository;
    private readonly IOperationRepository _operationRepository;
    private readonly ISessionService _sessionService;
    private readonly AccountService _sut;

    public AccountServiceTests()
    {
        _accountRepository = Substitute.For<IAccountRepository>();
        _operationRepository = Substitute.For<IOperationRepository>();
        _sessionService = Substitute.For<ISessionService>();
        _sut = new AccountService(_accountRepository, _operationRepository, _sessionService);
    }

    [Fact]
    public async Task DepositAsync_WithValidAmount_ShouldIncreaseBalance()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var sessionId = Guid.NewGuid();
        const decimal initialBalance = 100m;
        const decimal depositAmount = 50m;
        const decimal expectedBalance = 150m;

        var account = new Account("123456", "1234", initialBalance);

        _sessionService.ValidateSessionAsync(sessionId, false).Returns(true);
        _sessionService.GetAccountIdFromSessionAsync(sessionId).Returns(accountId);
        _accountRepository.GetByIdAsync(accountId).Returns(account);

        // Act
        AccountDto result = await _sut.DepositAsync(accountId, depositAmount, sessionId);

        // Assert
        Assert.Equal(expectedBalance, result.Balance);
        await _accountRepository.Received(1).UpdateAsync(Arg.Is<Account>(a => a.Balance == expectedBalance));
        await _operationRepository.Received(1).AddAsync(Arg.Is<Operation>(o =>
            o.Type == OperationType.Deposit &&
            o.Amount == depositAmount &&
            o.BalanceAfter == expectedBalance));
    }

    [Fact]
    public async Task DepositAsync_WithNegativeAmount_ShouldThrowArgumentException()
    {
        var accountId = Guid.NewGuid();
        var sessionId = Guid.NewGuid();
        const decimal invalidAmount = -50m;

        var account = new Account("123456", "1234", 100m);

        _sessionService.ValidateSessionAsync(sessionId, false).Returns(true);
        _sessionService.GetAccountIdFromSessionAsync(sessionId).Returns(accountId);
        _accountRepository.GetByIdAsync(accountId).Returns(account);

        await Assert.ThrowsAsync<ArgumentException>(() =>
            _sut.DepositAsync(accountId, invalidAmount, sessionId));

        await _accountRepository.DidNotReceive().UpdateAsync(Arg.Any<Account>());
    }

    [Fact]
    public async Task DepositAsync_WithZeroAmount_ShouldThrowArgumentException()
    {
        var accountId = Guid.NewGuid();
        var sessionId = Guid.NewGuid();
        const decimal invalidAmount = 0m;

        var account = new Account("123456", "1234", 100m);

        _sessionService.ValidateSessionAsync(sessionId, false).Returns(true);
        _sessionService.GetAccountIdFromSessionAsync(sessionId).Returns(accountId);
        _accountRepository.GetByIdAsync(accountId).Returns(account);

        await Assert.ThrowsAsync<ArgumentException>(() =>
            _sut.DepositAsync(accountId, invalidAmount, sessionId));
    }

    [Fact]
    public async Task WithdrawAsync_WithSufficientBalance_ShouldDecreaseBalance()
    {
        var accountId = Guid.NewGuid();
        var sessionId = Guid.NewGuid();
        const decimal initialBalance = 100m;
        const decimal withdrawAmount = 30m;
        const decimal expectedBalance = 70m;

        var account = new Account("123456", "1234", initialBalance);

        _sessionService.ValidateSessionAsync(sessionId, false).Returns(true);
        _sessionService.GetAccountIdFromSessionAsync(sessionId).Returns(accountId);
        _accountRepository.GetByIdAsync(accountId).Returns(account);

        // Act
        AccountDto result = await _sut.WithdrawAsync(accountId, withdrawAmount, sessionId);

        // Assert
        Assert.Equal(expectedBalance, result.Balance);
        await _accountRepository.Received(1).UpdateAsync(Arg.Is<Account>(a => a.Balance == expectedBalance));
        await _operationRepository.Received(1).AddAsync(Arg.Is<Operation>(o =>
            o.Type == OperationType.Withdrawal &&
            o.Amount == withdrawAmount &&
            o.BalanceAfter == expectedBalance));
    }

    [Fact]
    public async Task WithdrawAsync_WithInsufficientBalance_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var sessionId = Guid.NewGuid();
        const decimal initialBalance = 50m;
        const decimal withdrawAmount = 100m;

        var account = new Account("123456", "1234", initialBalance);

        _sessionService.ValidateSessionAsync(sessionId, false).Returns(true);
        _sessionService.GetAccountIdFromSessionAsync(sessionId).Returns(accountId);
        _accountRepository.GetByIdAsync(accountId).Returns(account);

        InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _sut.WithdrawAsync(accountId, withdrawAmount, sessionId));

        Assert.Contains("Insufficient funds", exception.Message, StringComparison.Ordinal);
        await _accountRepository.DidNotReceive().UpdateAsync(Arg.Any<Account>());
    }

    [Fact]
    public async Task WithdrawAsync_WithNegativeAmount_ShouldThrowArgumentException()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var sessionId = Guid.NewGuid();
        const decimal invalidAmount = -50m;

        var account = new Account("123456", "1234", 100m);

        _sessionService.ValidateSessionAsync(sessionId, false).Returns(true);
        _sessionService.GetAccountIdFromSessionAsync(sessionId).Returns(accountId);
        _accountRepository.GetByIdAsync(accountId).Returns(account);

        await Assert.ThrowsAsync<ArgumentException>(() =>
            _sut.WithdrawAsync(accountId, invalidAmount, sessionId));

        await _accountRepository.DidNotReceive().UpdateAsync(Arg.Any<Account>());
    }

    [Fact]
    public async Task WithdrawAsync_WithExactBalance_ShouldResultInZeroBalance()
    {
        var accountId = Guid.NewGuid();
        var sessionId = Guid.NewGuid();
        const decimal initialBalance = 100m;
        const decimal withdrawAmount = 100m;
        const decimal expectedBalance = 0m;

        var account = new Account("123456", "1234", initialBalance);

        _sessionService.ValidateSessionAsync(sessionId, false).Returns(true);
        _sessionService.GetAccountIdFromSessionAsync(sessionId).Returns(accountId);
        _accountRepository.GetByIdAsync(accountId).Returns(account);

        // Act
        AccountDto result = await _sut.WithdrawAsync(accountId, withdrawAmount, sessionId);

        // Assert
        Assert.Equal(expectedBalance, result.Balance);
        await _accountRepository.Received(1).UpdateAsync(Arg.Is<Account>(a => a.Balance == expectedBalance));
    }
}
