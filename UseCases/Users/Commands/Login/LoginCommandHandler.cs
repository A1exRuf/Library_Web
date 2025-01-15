using Core.Abstractions;
using UseCases.Abstractions.Messaging;

namespace UseCases.Users.Commands.Login;

internal sealed class LoginCommandHandler : ICommandHandler<LoginCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public LoginCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user == null || !_passwordHasher.VerifyPassword(user.PasswordHash, request.Password))
        {
            throw new UnauthorizedAccessException("Invalid Email or password");
        }
        return user.Id;
    }
}
