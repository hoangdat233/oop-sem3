using Lab5.Lab5.Application.Interfaces;
using Lab5.Lab5.Application.Services;

namespace Lab5.Lab5.Application.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ISessionService, SessionService>();

        return services;
    }
}