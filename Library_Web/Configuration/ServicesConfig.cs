using Azure.Storage.Blobs;
using Core.Abstractions;
using Infrastructure.Authentication;
using Infrastructure.Services;
using Library_Web.Middleware;

namespace Library_Web.Configuration;

public static class ServicesConfig
{
    public static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<IBlobService, BlobService>();

        services.AddSingleton(provider =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            string connectionString = configuration.GetConnectionString("BlobStorage");
            return new BlobServiceClient(connectionString);
        });

        services.AddTransient<ExceptionHandlingMiddleware>();
    }
}
