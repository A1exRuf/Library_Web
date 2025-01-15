using Core.Abstractions;
using Core.Entities;
using UseCases.Abstractions.Messaging;
using UseCases.Users.Commands.Login;

namespace UseCases.Users.Commands.Register;

internal class RegisterCommandHandler : ICommandHandler<RegisterCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterCommandHandler(IUserRepository userRepository, 
        IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (request.Password != request.ConfirmPassword)
        {
            throw new ArgumentException("Password aro not equal");
        }

        if (await _userRepository.EmailExistsAsync(request.Email))
        {
            throw new ArgumentException("User with this Email is already exists");
        }

        var hashedPassword = _passwordHasher.HashPassword(request.Password);

        var user = new User(Guid.NewGuid(), request.Name, request.Email, hashedPassword, request.Role);

        _userRepository.Insert(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
