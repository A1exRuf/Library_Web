using Core.Entities;

namespace Core.Abstractions;

public interface IBookRepository
{
    void Insert(Book book);
    void Delete(Book book);
    void Update(Book book);
    Task<Book?> GetByIdAsync(Guid bookId, CancellationToken cancellationToken);
}

