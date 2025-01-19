using UseCases.Abstractions.Messaging;

namespace UseCases.Books.Commands.ReturnBook;

public record ReturnBookCommand(Guid BookLoanId) : ICommand<bool>;