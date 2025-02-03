using Core.Common;
using Core.Entities;

namespace Core.Abstractions;

public interface IBookRepository
{
    Task<Book?> GetByIdAsync(Guid id);
    IQueryable<Book> Query();
    Task<PagedList<T>> GetPagedAsync<T>(IQueryable<T> query, int page, int pageSize);

    void Add(Book book);
    void Update(Book book);
    void Remove(Book book);
}

