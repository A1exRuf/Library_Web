using UseCases.Abstractions.Messaging;

namespace UseCases.Books.Commands.DeleteBook;

public sealed record DeleteBookCommand(Guid BookId) : ICommand<bool>;
