using Core.Abstractions;
using Infrastructure;
using Infrastructure.Repositories;

namespace Library_Web.Configuration;

public static class RepositoriesConfig
{
    public static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IAuthorRepository, AuthorRepository>();

        services.AddScoped<IBookRepository, BookRepository>();

        services.AddScoped<IBookLoanRepository, BookLoanRepository>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
