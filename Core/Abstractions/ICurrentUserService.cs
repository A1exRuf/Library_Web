namespace Core.Abstractions;

public interface ICurrentUserService
{
    Guid? UserId { get; }
    string? Role { get; }
}
