namespace UseCases.Users.Commands.Register;

public sealed record RegisterUserRequest(
    string Name,
    string Email,
    string Password,
    string ConfirmPassword,
    string Role);