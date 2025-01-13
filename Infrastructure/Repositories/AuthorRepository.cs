using Core.Abstractions;
using Core.Entities;

namespace Infrastructure.Repositories;

public sealed class AuthorRepository : IAuthorRepository
{
    private readonly ApplicationDbContext _dbContext;

    public AuthorRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public void Insert(Author author) => _dbContext.Set<Author>().Add(author);
}
