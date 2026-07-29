using Lab5.Lab5.Domain.Entities;

namespace Lab5.Lab5.Application.DTOs;

public class OperationDto
{
    public Guid Id { get; set; }

    public OperationType Type { get; set; }

    public decimal Amount { get; set; }

    public decimal BalanceAfter { get; set; }

    public DateTime Timestamp { get; set; }

    public string Description { get; set; } = string.Empty;
}