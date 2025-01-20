using UseCases.Abstractions.Messaging;

namespace UseCases.Users.Commands.UpdateUser;

public sealed record UpdateUserCommand(
    Guid UserId,
    string Name,
    string Email,
    string Password,
    string ConfirmPassword,
    string Role) : ICommand<bool>;
