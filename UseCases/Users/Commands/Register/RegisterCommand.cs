using UseCases.Abstractions.Messaging;

namespace UseCases.Users.Commands.Register;

public sealed record RegisterCommand(
    string Name,
    string Email, 
    string Password,
    string ConfirmPassword,
    string Role) : ICommand<Guid>;
