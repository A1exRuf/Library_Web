using UseCases.Abstractions.Messaging;

namespace UseCases.Users.Commands.DeleteUser;

public sealed record DeleteUserCommand(Guid UserId) : ICommand<bool>;
