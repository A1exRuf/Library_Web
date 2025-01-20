using Core.Abstractions;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class BookLoanRepository : IBookLoanRepository
{
    private readonly ApplicationDbContext _context;

    public BookLoanRepository(ApplicationDbContext context) => _context = context;


    public Task<BookLoan?> GetByIdAsync(Guid id)
    {
        return _context.BookLoans.SingleOrDefaultAsync(bl => bl.Id == id);
    }
    public void Add(BookLoan bookLoan) => _context.BookLoans.Add(bookLoan);
    public void Update(BookLoan bookLoan) => _context.BookLoans.Update(bookLoan);
    public void Remove(BookLoan bookLoan) => _context.BookLoans.Remove(bookLoan);
}
