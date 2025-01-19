using Core.Abstractions;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public void Insert(User User) => _dbContext.Set<User>().Add(User);

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _dbContext.Set<User>().AnyAsync(u => u.Email == email);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await _dbContext.Set<User>().FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<User>().FindAsync(new object[] { userId }, cancellationToken);
    }
}
