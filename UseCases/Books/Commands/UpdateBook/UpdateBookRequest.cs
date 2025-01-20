namespace UseCases.Books.Commands.UpdateBook;

public sealed record UpdateBookRequest(
    Guid BookId,
    string Isbn,
    string Title,
    string Genre,
    string Description,
    Guid AuthorId);
