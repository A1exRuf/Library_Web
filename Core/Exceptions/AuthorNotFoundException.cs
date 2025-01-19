using Core.Exceptions.Base;

namespace Core.Exceptions;

public sealed class AuthorNotFoundException : NotFoundException
{
    public AuthorNotFoundException(Guid AuthorId)
        : base($"The author with the identifier {AuthorId} was not found")
    {
    }
}