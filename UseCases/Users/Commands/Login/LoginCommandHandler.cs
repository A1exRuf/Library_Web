using Core.Abstractions;
using Core.Entities;
using Core.Filters;
using UseCases.Abstractions.Messaging;

namespace UseCases.Users.Commands.Login;

internal sealed class LoginCommandHandler : ICommandHandler<LoginCommand, LoginResponse>
{
    private readonly IRepository<User> _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;

    public LoginCommandHandler(
        IRepository<User> userRepository, 
        IRefreshTokenRepository refreshTokenRepository, 
        IPasswordHasher passwordHasher, 
        ITokenService tokenService,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
    }

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // Getting user
        var user = await _userRepository.GetAsync(
            new UsersFilter { Email = request.Email },
            cancellationToken: cancellationToken);

        if (user == null || !_passwordHasher.VerifyPassword(user.PasswordHash, request.Password))
        {
            throw new UnauthorizedAccessException("Invalid Email or password");
        }

        // Generatin token
        var accessToken = _tokenService.GenerateAccessToken(user);
        var refreshToken = new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = _tokenService.GenerateRefreshToken(),
            ExpiresOnUtc = DateTime.UtcNow.AddDays(7)
        };

        // Removing old token and adding new
        await _refreshTokenRepository.Remove(refreshToken.UserId);

        _refreshTokenRepository.Add(refreshToken);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new LoginResponse(accessToken, refreshToken.Token);

    }
}