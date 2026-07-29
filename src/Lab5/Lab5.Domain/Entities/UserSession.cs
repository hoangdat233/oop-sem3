namespace Lab5.Lab5.Domain.Entities;

public class UserSession
{
    public Guid Id { get; private set; }

    public Guid? AccountId { get; private set; }

    public bool IsAdmin { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime ExpiresAt { get; private set; }

    public UserSession(Guid? accountId, bool isAdmin, int durationMinutes = 30)
    {
        Id = Guid.NewGuid();
        AccountId = accountId;
        IsAdmin = isAdmin;
        CreatedAt = DateTime.UtcNow;
        ExpiresAt = CreatedAt.AddMinutes(durationMinutes);
    }

    internal UserSession(Guid id, Guid? accountId, bool isAdmin, DateTime createdAt, DateTime expiresAt)
    {
        Id = id;
        AccountId = accountId;
        IsAdmin = isAdmin;
        CreatedAt = createdAt;
        ExpiresAt = expiresAt;
    }

    private UserSession() { }

    public bool IsValid()
    {
        return DateTime.UtcNow < ExpiresAt;
    }
}
