using Core.Entities;

namespace UseCases.Books.Commands.CreateBook;

public sealed record CreateBookRequest(string Isbn, string Title,
        string Genree, string Description, Guid AuthorId);
