using Core.Exceptions.Base;

namespace UseCases.Exceptions;

public sealed class RefreshTokenExpiredException : BadRequestException
{
    public RefreshTokenExpiredException() : base("Refresh token expired")
    {
    }
}
