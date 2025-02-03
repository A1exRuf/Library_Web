using Core.Abstractions;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public sealed class AuthorRepository : IAuthorRepository
{
    private readonly ApplicationDbContext _context;

    public AuthorRepository(ApplicationDbContext context) => _context = context;

    public async Task<Author?> GetByIdAsync(Guid id) =>
        await _context.Authors.SingleOrDefaultAsync(a => a.Id == id);

    public async Task<List<T>> GetAllAsync<T>(Expression<Func<Author, T>> selector) => 
        await _context.Authors.Select(selector).ToListAsync();

    public void Add(Author author) => _context.Authors.Add(author);
    public void Update(Author author) => _context.Authors.Update(author);
    public void Remove(Author author) => _context.Authors.Remove(author);
}