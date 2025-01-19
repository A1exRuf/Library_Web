using Core.Abstractions;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class BookLoanRepository : IBookLoanRepository
{
    private readonly ApplicationDbContext _dbContext;

    public BookLoanRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public void Insert(BookLoan bookLoan) => _dbContext.Set<BookLoan>().Add(bookLoan);

    public void Delete(BookLoan bookLoan)
    {
        _dbContext.Set<BookLoan>().Remove(bookLoan);
    }

    public async Task<BookLoan?> GetByIdAsync(Guid bookLoanId, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<BookLoan>().FindAsync(new object[] { bookLoanId }, cancellationToken);
    }
}
