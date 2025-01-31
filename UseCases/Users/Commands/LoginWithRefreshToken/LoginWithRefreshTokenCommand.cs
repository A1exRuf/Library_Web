using UseCases.Abstractions.Messaging;

namespace UseCases.Users.Commands.LoginWithRefreshToken;

public sealed record LoginWithRefreshTokenCommand(string RefreshToken) : ICommand<LoginWithRefreshTokenResponse>;

