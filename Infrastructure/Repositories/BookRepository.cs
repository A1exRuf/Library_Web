using Core.Abstractions;
using Core.Common;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class BookRepository : IBookRepository
{
    private readonly ApplicationDbContext _context;

    public BookRepository(ApplicationDbContext context) => _context = context;

    public Task<Book?> GetByIdAsync(Guid id) => 
        _context.Books.Include(b => b.Author).SingleOrDefaultAsync(b => b.Id == id);

    public IQueryable<Book> Query() => 
        _context.Books;

    public async Task<PagedList<T>> GetPagedAsync<T>(
        IQueryable<T> query, int page, int pageSize) =>
        await PagedList<T>.CreateAsync(query, page, pageSize);
    

    public void Add(Book book) => _context.Books.Add(book);
    public void Update(Book book) => _context.Books.Update(book);
    public void Remove(Book book) => _context.Books.Remove(book);

}
