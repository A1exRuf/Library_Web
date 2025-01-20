namespace UseCases.Users.Commands.UpdateUser;

public sealed record UpdateUserRequest(
    Guid UserId,
    string Name,
    string Email,
    string Password,
    string ConfirmPassword,
    string Role);
