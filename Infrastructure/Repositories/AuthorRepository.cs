using Core.Abstractions;
using Core.Entities;

namespace Infrastructure.Repositories;

public sealed class AuthorRepository : IAuthorRepository
{
    private readonly ApplicationDbContext _dbContext;

    public AuthorRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public void Insert(Author author) => _dbContext.Set<Author>().Add(author);

    public void Delete(Author author)
    {
        _dbContext.Set<Author>().Remove(author);
    }

    public async Task<Author?> GetByIdAsync(Guid authorId, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<Author>().FindAsync(new object[] { authorId }, cancellationToken);
    }
}