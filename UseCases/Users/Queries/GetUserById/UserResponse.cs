namespace UseCases.Users.Queries.GetUserById;

public sealed record UserResponse(Guid id, string name, string email, string passwordHash, string role);
