using Lab5.Lab5.Domain.Entities;

namespace Lab5.Lab5.Infrastructure.Repositories;

internal static class SessionExtensions
{
    public static UserSession Copy(this UserSession session) =>
        new(session.Id, session.AccountId, session.IsAdmin, session.CreatedAt, session.ExpiresAt);
}
