namespace UseCases.Users.Commands.Login;

public sealed record LoginResponse(string AccessToken, string RefreshToken);

