using Lab5.Lab5.Domain.Entities;

namespace Lab5.Lab5.Infrastructure.Repositories;

internal static class AccountExtensions
{
    public static Account Copy(this Account account) =>
        new(account.Id, account.AccountNumber, account.PinCode, account.Balance, account.CreatedAt);
}
