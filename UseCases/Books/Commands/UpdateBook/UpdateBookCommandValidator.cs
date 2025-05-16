using Core.Abstractions;
using FluentValidation;

namespace UseCases.Books.Commands.UpdateBook;

public sealed class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(x => x.BookId);

        RuleFor(x => x.Isbn);

        RuleFor(x => x.Title);

        RuleFor(x => x.Genree).Must(g =>
        g == null ||
        g == "Fiction" ||
        g == "Non-Fiction" ||
        g == "Mystery/Thriller" ||
        g == "Science Fiction" ||
        g == "Fantasy" ||
        g == "Biography" ||
        g == "Romance");

        RuleFor(x => x.Description);

        RuleFor(x => x.AuthorId);
    }
}
