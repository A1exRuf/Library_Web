using Microsoft.AspNetCore.Http;

namespace UseCases.Books.Commands.UpdateBook;

public sealed record UpdateBookRequest(
    Guid BookId,
    string? Isbn,
    string? Title,
    string? Genree,
    string? Description,
    Guid? AuthorId,
    IFormFile? Image);
