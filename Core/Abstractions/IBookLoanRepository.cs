using Core.Entities;

namespace Core.Abstractions;

public interface IBookLoanRepository
{
    Task<BookLoan?> GetByIdAsync(Guid id);
    void Add(BookLoan bookLoan);
    void Update(BookLoan bookLoan);
    void Remove(BookLoan bookLoan);
}
