namespace Lab5.Lab5.Application.DTOs;

public class UserLoginRequest
{
    public string AccountNumber { get; set; } = string.Empty;

    public string PinCode { get; set; } = string.Empty;
}
