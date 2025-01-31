namespace UseCases.Users.Commands.LoginWithRefreshToken;

internal sealed record LoginWithRefreshTokenResponse(string AccessToken, string RefreshToken);