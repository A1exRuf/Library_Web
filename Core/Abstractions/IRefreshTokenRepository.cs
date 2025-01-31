using Core.Entities;

namespace Core.Abstractions;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetRefreshToken(string Token);
    Task Remove(Guid UserId);
    void Add(RefreshToken RefreshToken);
    void Update(RefreshToken RefreshToken);
}
