using UseCases.Abstractions.Messaging;

namespace UseCases.Books.Commands.CreateBook;

public sealed record CreateBookCommand(
    string Isbn,
    string Title,
    string Genree,
    string Description,
    Guid AuthorId) : ICommand<Guid>;
