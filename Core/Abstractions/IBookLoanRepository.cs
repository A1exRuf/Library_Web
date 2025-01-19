using Core.Entities;

namespace Core.Abstractions;

public interface IBookLoanRepository
{
    void Insert(BookLoan bookLoan);

    void Delete(BookLoan bookLoan);

    Task<BookLoan?> GetByIdAsync(Guid bookLoanId, CancellationToken cancellationToken);
}
