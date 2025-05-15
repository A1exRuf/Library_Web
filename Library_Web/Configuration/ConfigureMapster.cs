using Core.Common;
using Core.Entities;
using Mapster;
using UseCases.Authors.Queries;
using UseCases.BookLoans.Queries;
using UseCases.Books.Queries;
using UseCases.Users.Queries;

namespace Library_Web.Configuration;

public static class ConfigureMapster
{
    public static void Configure()
    {
        TypeAdapterConfig<Book, BookResponse>.NewConfig()
            .Map(dest => dest.Links, _ => new List<Link>());

        TypeAdapterConfig<Author, AuthorResponse>.NewConfig()
            .Map(dest => dest.Links, _ => new List<Link>());

        TypeAdapterConfig<BookLoan, BookLoanResponse>.NewConfig()
            .Map(dest => dest.Links, _ => new List<Link>());

        TypeAdapterConfig<User, UserResponse>.NewConfig()
            .Map(dest => dest.Links, _ => new List<Link>());
    }
}
