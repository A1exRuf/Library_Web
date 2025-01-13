using Core.Exceptions.Base;

namespace Core.Exceptions;

public sealed class AuthorNotFoundException : NotFoundException
{
    public AuthorNotFoundException(Guid authorId)
        : base($"The author with the identifier {authorId} was not found")
    {
    }
}