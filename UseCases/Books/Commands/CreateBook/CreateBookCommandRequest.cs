using Microsoft.AspNetCore.Http;

namespace UseCases.Books.Commands.CreateBook;

public sealed record CreateBookRequest(
    string Isbn,
    string Title,
    string Genree,
    string Description,
    Guid AuthorId,
    IFormFile? Image);
