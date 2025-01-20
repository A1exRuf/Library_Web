using Core.Entities;

namespace Core.Abstractions;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid userId);
    Task<User?> GetByEmailAsync(string email);
    Task<bool> EmailExistsAsync(string email);
    void Add(User User);
    void Update(User User);
    void Remove(User User);
}
