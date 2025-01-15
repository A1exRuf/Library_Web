namespace UseCases.Users.Commands.Login;

public sealed class LoginResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}

