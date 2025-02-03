using Core.Common;
using Core.Entities;
using System.Linq.Expressions;

namespace Core.Abstractions;

public interface IAuthorRepository
{
    Task<Author?> GetByIdAsync(Guid id);
    Task<List<T>> GetAllAsync<T>(Expression<Func<Author, T>> selector);
    void Add(Author author);
    void Update(Author author);
    void Remove(Author author);
}