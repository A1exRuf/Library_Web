using Core.Abstractions;
using FluentValidation;

namespace UseCases.Authors.Commands.UpdateAuthor;

public sealed class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
    private readonly IAuthorRepository _authorRepository;

    public UpdateAuthorCommandValidator(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;

        RuleFor(x => x.AuthorId).NotEmpty();

        RuleFor(x => x.FirstName);

        RuleFor(x => x.LastName);

        RuleFor(x => x.DateOfBirth);

        RuleFor(x => x.Country);
    }
}