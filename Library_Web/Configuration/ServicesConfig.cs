using Azure.Storage.Blobs;
using Core.Abstractions;
using Infrastructure.Authentication;
using Infrastructure.Services;
using Library_Web.Middleware;
using Presentation.Services;

namespace Library_Web.Configuration;

public static class ServicesConfig
{
    public static void AddServices(IServiceCollection services)
    {
        services.AddSingleton(provider =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            string connectionString = configuration.GetConnectionString("BlobStorage");
            return new BlobServiceClient(connectionString);
        });

        services.AddScoped<IPasswordHasher, PasswordHasher>();

        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<IBlobService, BlobService>();

        services.AddHttpContextAccessor();

        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<ILinkService, LinkService>();

        services.AddTransient<ExceptionHandlingMiddleware>();
    }
}
