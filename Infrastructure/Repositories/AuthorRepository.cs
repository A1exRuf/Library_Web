using Core.Abstractions;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class AuthorRepository : IAuthorRepository
{
    private readonly ApplicationDbContext _context;

    public AuthorRepository(ApplicationDbContext context) => _context = context;

    public Task<Author?> GetByIdAsync(Guid id)
    {
        return _context.Authors.SingleOrDefaultAsync(a => a.Id == id);
    }
    public void Add(Author author) => _context.Authors.Add(author);
    public void Update(Author author) => _context.Authors.Update(author);
    public void Remove(Author author) => _context.Authors.Remove(author);
}