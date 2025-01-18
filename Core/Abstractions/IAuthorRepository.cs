using Core.Entities;

namespace Core.Abstractions;

public interface IAuthorRepository
{
    void Insert(Author author);
    void Delete(Author author);
    Task<Author?> GetByIdAsync(Guid authorId, CancellationToken cancellationToken);
}