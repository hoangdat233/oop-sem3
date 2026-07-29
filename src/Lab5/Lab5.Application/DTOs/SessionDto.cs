namespace Lab5.Lab5.Application.DTOs;

public class SessionDto
{
    public Guid SessionId { get; set; }

    public DateTime ExpiresAt { get; set; }

    public bool IsAdmin { get; set; }
}