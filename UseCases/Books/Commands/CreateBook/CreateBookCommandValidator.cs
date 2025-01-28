using FluentValidation;

namespace UseCases.Books.Commands.CreateBook;

public sealed class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(x => x.Isbn).NotEmpty();

        RuleFor(x => x.Title).NotEmpty();

        RuleFor(x => x.Genree).NotEmpty().Must(g => 
        g == "Fiction" || 
        g == "Non-Fiction" ||
        g == "Mystery/Thriller" ||
        g == "Science Fiction" ||
        g == "Fantasy" ||
        g == "Biography" ||
        g == "Romance");

        RuleFor(x => x.Description).NotEmpty();

        RuleFor(x => x.AuthorId).NotEmpty();
    }
}
