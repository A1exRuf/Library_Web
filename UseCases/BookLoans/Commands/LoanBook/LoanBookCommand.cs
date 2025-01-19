using UseCases.Abstractions.Messaging;

namespace UseCases.Books.Commands.LoanBook;

public record LoanBookCommand(Guid BookId, Guid UserId) : ICommand<Guid>;