namespace Library_Web.Configuration;

public static class CorsConfig
{
    public const string AllowSpecificOrigins = "_AllowSpecificOrigins";
    public static void AddCors(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(name: AllowSpecificOrigins,
                builder =>
                {
                    builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                });
        });

    }
}
