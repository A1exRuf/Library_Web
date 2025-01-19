using Core.Exceptions.Base;

namespace Core.Exceptions;

public sealed class BookLoanNotFoundException : NotFoundException
{
    public BookLoanNotFoundException(Guid BookLoanId)
        : base($"The book with the identifier {BookLoanId} was not found")
    {
    }
}