namespace Core.Exceptions;

public sealed class BookNotAvailableException : Exception
{
    public BookNotAvailableException(Guid BookId)
        : base($"The book with the identifier {BookId} is not available")
    {
    }
}
