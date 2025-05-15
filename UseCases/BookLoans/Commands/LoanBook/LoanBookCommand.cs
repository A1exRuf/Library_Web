using UseCases.Abstractions.Messaging;

namespace UseCases.Books.Commands.LoanBook;

public record LoanBookCommand(Guid BookId) : ICommand<Guid>;