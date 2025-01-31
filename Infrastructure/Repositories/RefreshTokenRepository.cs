using Core.Abstractions;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly ApplicationDbContext _context;

    public RefreshTokenRepository(ApplicationDbContext context) => _context = context;

    public Task<RefreshToken?> GetRefreshToken(string Token)
    {
        return _context.RefreshTokens
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Token == Token);
    }
    public async Task Remove(Guid userId)
    {
        await _context.RefreshTokens
            .Where(r => r.UserId == userId)
            .ExecuteDeleteAsync();
    }

    public void Add(RefreshToken refreshToken) => _context.RefreshTokens.Add(refreshToken);
    public void Update(RefreshToken refreshToken) => _context.RefreshTokens.Update(refreshToken);
}
