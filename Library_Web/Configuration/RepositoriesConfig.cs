using Core.Abstractions;
using Infrastructure;
using Infrastructure.Repositories;

namespace Library_Web.Configuration;

public static class RepositoriesConfig
{
    public static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
