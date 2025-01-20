using Core.Entities;

namespace Core.Abstractions;

public interface IAuthorRepository
{
    Task<Author?> GetByIdAsync(Guid id);
    void Add(Author author);
    void Update(Author author);
    void Remove(Author author);
}