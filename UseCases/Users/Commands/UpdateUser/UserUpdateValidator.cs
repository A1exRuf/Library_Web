using FluentValidation;

namespace UseCases.Users.Commands.Register;

public sealed class UpdateUserValidator : AbstractValidator<RegisterCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Name);
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).MinimumLength(6);
        RuleFor(x => x.ConfirmPassword).Equal(x => x.Password);
        RuleFor(x => x.Role).Must(Role => Role == "Admin" || Role == "User");
    }
}