using System.Data;
using UseCases.Behaviors;
using Core.Abstractions;
using FluentValidation;
using Infrastructure;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Library_Web.Middleware;
using Microsoft.AspNetCore.Authentication.Cookies;
using Infrastructure.Services;

namespace Library_Web;

public class Startup
{
    public Startup(IConfiguration configuration) => Configuration = configuration;

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        var presentationAssembly = typeof(Presentation.AssemblyReference).Assembly;

        services.AddControllers()
            .AddApplicationPart(presentationAssembly);

        var applicationAssembly = typeof(UseCases.AssemblyReference).Assembly;

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssembly(applicationAssembly);

        services.AddSwaggerGen();

        services.AddDbContext<ApplicationDbContext>(builder =>
            builder.UseNpgsql(Configuration.GetConnectionString("Application")));

        services.AddScoped<IAuthorRepository, AuthorRepository>();

        services.AddScoped<IBookRepository, BookRepository>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUnitOfWork>(
            factory => factory.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IDbConnection>(
            factory => factory.GetRequiredService<ApplicationDbContext>().Database.GetDbConnection());

        services.AddScoped<IPasswordHasher, PasswordHasher>();

        services.AddTransient<ExceptionHandlingMiddleware>();

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(option =>
            {
                option.LogoutPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
            });

        services.AddAuthorization(opts => {
            opts.AddPolicy("OnlyForAdmin", policy => {
                policy.RequireClaim("role", "Admin");
            });
            opts.AddPolicy("OnlyForUser", policy =>
            {
                policy.RequireClaim("role", "User");
            });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

            app.UseSwagger();

            app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}