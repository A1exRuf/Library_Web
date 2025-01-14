using Core.Abstractions;
using Core.Entities;

namespace Infrastructure.Repositories;

public sealed class BookRepository : IBookRepository
{
    private readonly ApplicationDbContext _dbContext;

    public BookRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public void Insert(Book book) => _dbContext.Set<Book>().Add(book);

    public void Delete(Book book)
    {
        _dbContext.Set<Book>().Remove(book);
    }

    public async Task<Book?> GetByIdAsync(Guid bookId, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<Book>().FindAsync(new object[] { bookId }, cancellationToken);
    }
}
