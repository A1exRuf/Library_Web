using Core.Abstractions;
using FluentValidation;

namespace UseCases.Books.Commands.UpdateBook;

public sealed class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator(IBookRepository bookRepository)
    {
        RuleFor(x => x.BookId).NotEmpty();

        RuleFor(x => x.Isbn).NotEmpty();

        RuleFor(x => x.Title).NotEmpty();

        RuleFor(x => x.Genree).NotEmpty();

        RuleFor(x => x.Description).NotEmpty();

        RuleFor(x => x.AuthorId).NotEmpty();
    }
}
