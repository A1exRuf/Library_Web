using Core.Abstractions;
using Core.Common;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public sealed class BookLoanRepository : IBookLoanRepository
{
    private readonly ApplicationDbContext _context;

    public BookLoanRepository(ApplicationDbContext context) => _context = context;

    public async Task<BookLoan?> GetByIdAsync(Guid id) => 
        await _context.BookLoans.Include(bl => bl.Book).SingleOrDefaultAsync(bl => bl.Id == id);

    public async Task<PagedList<T>> GetByUserIdAsync<T>(
        Guid userId, 
        Expression<Func<BookLoan, T>> selector, 
        int page, 
        int pageSize)
    {
        var query = _context.BookLoans
            .Where(bl => bl.UserId == userId)
            .Select(selector);

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PagedList<T>(items, page, pageSize, totalCount);
    }

    public void Add(BookLoan bookLoan) => _context.BookLoans.Add(bookLoan);
    public void Update(BookLoan bookLoan) => _context.BookLoans.Update(bookLoan);
    public void Remove(BookLoan bookLoan) => _context.BookLoans.Remove(bookLoan);
}
