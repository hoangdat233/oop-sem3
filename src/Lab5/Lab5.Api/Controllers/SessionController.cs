using Lab5.Lab5.Application.DTOs;
using Lab5.Lab5.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Lab5.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SessionController : ControllerBase
{
    private readonly ISessionService _sessionService;
    private readonly string _adminPassword;

    public SessionController(ISessionService sessionService, IConfiguration configuration)
    {
        _sessionService = sessionService;
        _adminPassword = configuration["AdminPassword"] ?? "admin123";
    }

    [HttpPost("user/login")]
    public async Task<IActionResult> UserLogin([FromBody] UserLoginRequest request)
    {
        try
        {
            SessionDto session = await _sessionService.CreateUserSessionAsync(request);
            return Ok(session);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Invalid account number or PIN");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("admin/login")]
    public async Task<IActionResult> AdminLogin([FromBody] AdminLoginRequest request)
    {
        try
        {
            SessionDto session = await _sessionService.CreateAdminSessionAsync(request, _adminPassword);
            return Ok(session);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Invalid admin password");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}