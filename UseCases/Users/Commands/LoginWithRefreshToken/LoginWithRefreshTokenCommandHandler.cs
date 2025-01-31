using Core.Abstractions;
using Core.Entities;
using UseCases.Abstractions.Messaging;
using UseCases.Exceptions;
using UseCases.Users.Commands.Login;

namespace UseCases.Users.Commands.LoginWithRefreshToken;

internal sealed class LoginWithRefreshTokenCommandHandler : ICommandHandler<LoginWithRefreshTokenCommand, LoginWithRefreshTokenResponse>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;

    public LoginWithRefreshTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository, ITokenService tokenService, IUnitOfWork unitOfWork)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
    }

    public async Task<LoginWithRefreshTokenResponse> Handle(LoginWithRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        RefreshToken? refreshToken = await _refreshTokenRepository.GetRefreshToken(request.RefreshToken);

        if (refreshToken == null || refreshToken.ExpiresOnUtc < DateTime.UtcNow) 
        {
            throw new RefreshTokenExpiredException();
        }

        string accessToken = _tokenService.GenerateAccessToken(refreshToken.User);

        refreshToken.Token = _tokenService.GenerateRefreshToken();
        refreshToken.ExpiresOnUtc = DateTime.UtcNow.AddDays(7);

        _refreshTokenRepository.Update(refreshToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new LoginWithRefreshTokenResponse(accessToken, refreshToken.Token);
    }
}
