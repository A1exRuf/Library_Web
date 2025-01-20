using Core.Abstractions;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class BookRepository : IBookRepository
{
    private readonly ApplicationDbContext _context;

    public BookRepository(ApplicationDbContext context) => _context = context;

    public Task<Book?> GetByIdAsync(Guid id)
    {
        return _context.Books.SingleOrDefaultAsync(b => b.Id == id);
    }

    public void Add(Book book) => _context.Books.Add(book);
    public void Update(Book book) => _context.Books.Update(book);
    public void Remove(Book book) => _context.Books.Remove(book);
}
