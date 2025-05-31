using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using Core.Filters;
using UseCases.Abstractions.Messaging;

namespace UseCases.Users.Commands.UpdateUser;

internal class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, bool>
{
    private readonly IRepository<User> _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(
        IRepository<User> userRepository, 
        IPasswordHasher passwordHasher, 
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        // Getting user
        var user = await _userRepository.GetAsync(
            new UsersFilter { Id = request.UserId},
            asNoTracking: false,
            cancellationToken) ?? throw 
            new UserNotFoundException(request.UserId);

        // Hashing new password if exists
        string? hashedPassword = null;

        if(request.Password != null)
            hashedPassword = _passwordHasher.HashPassword(request.Password);

        // Updating user
        user.Name = request.Name ?? user.Name;
        user.Email = request.Email ?? user.Email;
        user.PasswordHash = hashedPassword ?? user.PasswordHash;
        user.Role = request.Role ?? user.Role;

        _userRepository.Update(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
