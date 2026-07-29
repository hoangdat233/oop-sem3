using Lab5.Lab5.Domain.Interfaces;
using Lab5.Lab5.Infrastructure.Repositories;

namespace Lab5.Lab5.Infrastructure.Extensions;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<IAccountRepository, AccountRepository>();
        services.AddSingleton<IOperationRepository, OperationRepository>();
        services.AddSingleton<ISessionRepository, SessionRepository>();

        return services;
    }
}
