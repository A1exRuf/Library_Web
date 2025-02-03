using Core.Common;
using Core.Entities;

namespace Core.Abstractions;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid userId);
    Task<User?> GetByEmailAsync(string email);
    Task<bool> EmailExistsAsync(string email);
    IQueryable<User> Query();
    Task<PagedList<T>> GetPagedAsync<T>(IQueryable<T> query, int page, int pageSize);

    void Add(User User);
    void Update(User User);
    void Remove(User User);
}
