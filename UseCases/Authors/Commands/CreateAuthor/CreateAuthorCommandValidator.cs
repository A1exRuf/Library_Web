using FluentValidation;

namespace UseCases.Authors.Commands.CreateAuthor;

public sealed class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();

        RuleFor(x => x.SecondName).NotEmpty();

        RuleFor(x => x.DateOfBirth).NotEmpty();

        RuleFor(x => x.Country).NotEmpty();
    }
}
