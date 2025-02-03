using Core.Abstractions;
using Core.Common;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context) => _context = context;

    public async Task<bool> EmailExistsAsync(string email) => 
        await _context.Users.AnyAsync(u => u.Email == email);
  
    public async Task<User?> GetByEmailAsync(string email) =>
        await _context.Users.SingleOrDefaultAsync(u => u.Email == email);

    public async Task<User?> GetByIdAsync(Guid id) =>
        await _context.Users.SingleOrDefaultAsync(u => u.Id == id);

    public IQueryable<User> Query() => 
        _context.Users;

    public async Task<PagedList<T>> GetPagedAsync<T>(
        IQueryable<T> query, int page, int pageSize) =>
        await PagedList<T>.CreateAsync(query, page, pageSize);

    public void Add(User User) => _context.Users.Add(User);
    public void Update(User User) => _context.Users.Update(User);
    public void Remove(User User) => _context.Users.Remove(User);
}
