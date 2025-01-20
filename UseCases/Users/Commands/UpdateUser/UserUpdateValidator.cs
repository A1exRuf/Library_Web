using FluentValidation;

namespace UseCases.Users.Commands.Register;

public sealed class UpdateUserValidator : AbstractValidator<RegisterCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
        RuleFor(x => x.ConfirmPassword).Equal(x => x.Password);
        RuleFor(x => x.Role).Must(Role => Role == "Admin" || Role == "User");
    }
}