using Core.Abstractions;
using Core.Exceptions;
using UseCases.Abstractions.Messaging;

namespace UseCases.Users.Commands.UpdateUser;

internal class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user == null)
        {
            throw new UserNotFoundException(request.UserId);
        }

        string? hashedPassword = null;

        if(request.Password != null)
        {
            hashedPassword = _passwordHasher.HashPassword(request.Password);
        }

        user.Name = request.Name ?? user.Name;
        user.Email = request.Email ?? user.Email;
        user.PasswordHash = hashedPassword ?? user.PasswordHash;
        user.Role = request.Role;

        _userRepository.Update(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
