using FluentValidation;
using Core.Abstractions;

namespace UseCases.Authors.Commands.UpdateAuthor;

public sealed class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
    private readonly IAuthorRepository _authorRepository;

    public UpdateAuthorCommandValidator(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;

        RuleFor(x => x.AuthorId).NotEmpty();

        RuleFor(x => x.FirstName).NotEmpty();

        RuleFor(x => x.LastName).NotEmpty();

        RuleFor(x => x.DateOfBirth).NotEmpty();

        RuleFor(x => x.Country).NotEmpty();
    }
}