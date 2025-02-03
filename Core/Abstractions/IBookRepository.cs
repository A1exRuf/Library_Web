using Core.Entities;

namespace Core.Abstractions;

public interface IBookRepository
{
    Task<Book?> GetByIdAsync(Guid id);

    void Add(Book book);
    void Update(Book book);
    void Remove(Book book);
}

