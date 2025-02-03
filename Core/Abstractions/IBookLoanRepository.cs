using Core.Common;
using Core.Entities;
using System.Linq.Expressions;

namespace Core.Abstractions;

public interface IBookLoanRepository
{
    Task<BookLoan?> GetByIdAsync(Guid id);
    Task<PagedList<T>> GetByUserIdAsync<T>(
        Guid userId,
        Expression<Func<BookLoan, T>> selector,
        int page,
        int pageSize);

    void Add(BookLoan bookLoan);
    void Update(BookLoan bookLoan);
    void Remove(BookLoan bookLoan);
}
