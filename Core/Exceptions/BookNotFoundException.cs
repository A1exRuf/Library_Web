using Core.Exceptions.Base;

namespace Core.Exceptions;

public sealed class BookNotFoundException : NotFoundException
{
    public BookNotFoundException(Guid BookId)
        : base($"The author with the identifier {BookId} was not found")
    {
    }
}