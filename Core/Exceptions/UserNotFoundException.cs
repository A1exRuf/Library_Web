using Core.Exceptions.Base;

namespace Core.Exceptions;

public sealed class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(Guid UserId)
        : base($"The user with the identifier {UserId} was not found")
    {
    }
}
