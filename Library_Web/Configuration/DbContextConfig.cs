using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Library_Web.Configuration;

public static class DbContextConfig
{
    public static void AddDatabase(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(builder =>
            builder.UseNpgsql(configuration.GetConnectionString("Application")));
    }
}
