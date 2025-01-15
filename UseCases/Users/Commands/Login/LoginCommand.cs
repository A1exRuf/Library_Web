using UseCases.Abstractions.Messaging;

namespace UseCases.Users.Commands.Login;

public sealed record LoginCommand(string Email, string Password) : ICommand<Guid>;
