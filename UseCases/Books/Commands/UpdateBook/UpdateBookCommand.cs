using UseCases.Abstractions.Messaging;

namespace UseCases.Books.Commands.UpdateBook;

public sealed record UpdateBookCommand(
    Guid BookId,
    string? Isbn,
    string? Title,
    string? Genree,
    string? Description,
    Guid? AuthorId,
    Stream? ImageStream) : ICommand<bool>;
