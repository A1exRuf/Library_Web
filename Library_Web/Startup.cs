using Library_Web.Configuration;
using Library_Web.Extensions;
using Library_Web.Middleware;

namespace Library_Web;

public class Startup
{
    public Startup(IConfiguration configuration) => Configuration = configuration;

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        MediatRConfig.AddMediatr(services);
        SwaggerConfig.AddSwager(services);
        DbContextConfig.AddDatabase(services, Configuration);
        RepositoriesConfig.AddRepositories(services);
        ServicesConfig.AddServices(services);
        JwtAuthConfig.AddJwtAuth(services, Configuration);
        CorsConfig.AddCors(services);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.ApplyMigrations();
        }

        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors(CorsConfig.AllowSpecificOrigins);
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}