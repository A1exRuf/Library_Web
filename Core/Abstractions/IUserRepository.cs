using Core.Entities;

namespace Core.Abstractions;

public interface IUserRepository
{
    void Insert(User User);
    Task<bool> EmailExistsAsync(string email);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task<User> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken);
}
