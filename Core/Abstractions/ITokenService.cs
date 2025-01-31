using Core.Entities;

namespace Core.Abstractions;

public interface ITokenService
{
    string GenerateAccessToken(User user);

    string GenerateRefreshToken();
}
