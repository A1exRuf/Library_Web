using Core.Abstractions;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context) => _context = context;

    public Task<bool> EmailExistsAsync(string email)
    {
        return _context.Users.AnyAsync(u => u.Email == email);
    }

    public Task<User?> GetByEmailAsync(string email)
    {
        return _context.Users.SingleOrDefaultAsync(u => u.Email == email);
    }

    public Task<User?> GetByIdAsync(Guid id)
    {
        return _context.Users.SingleOrDefaultAsync(u => u.Id == id);
    }

    public void Add(User User) => _context.Users.Add(User);
    public void Update(User User) => _context.Users.Update(User);
    public void Remove(User User) => _context.Users.Remove(User);
}
