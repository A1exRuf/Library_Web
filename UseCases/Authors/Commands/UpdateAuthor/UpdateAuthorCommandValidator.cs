using Core.Abstractions;
using Core.Entities;
using FluentValidation;

namespace UseCases.Authors.Commands.UpdateAuthor;

public sealed class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidator()
    {
        RuleFor(x => x.AuthorId).NotEmpty();

        RuleFor(x => x.FirstName);

        RuleFor(x => x.LastName);

        RuleFor(x => x.DateOfBirth);

        RuleFor(x => x.Country);
    }
}