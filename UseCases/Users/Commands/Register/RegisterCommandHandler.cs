using Core.Abstractions;
using Core.Entities;
using Core.Filters;
using UseCases.Abstractions.Messaging;
using UseCases.Exceptions;

namespace UseCases.Users.Commands.Register;

internal class RegisterCommandHandler : ICommandHandler<RegisterCommand, Guid>
{
    private readonly IRepository<User> _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterCommandHandler(
        IRepository<User> userRepository, 
        IUnitOfWork unitOfWork, 
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        // Cheking if Email allready taken
        bool emailTaken = await _userRepository
            .ExistsAsync( new UsersFilter { Email = request.Email });

        if (emailTaken)
            throw new EmailExistsException();

        // Creating new User
        var hashedPassword = _passwordHasher.HashPassword(request.Password);

        var user = new User(Guid.NewGuid(), request.Name, request.Email, hashedPassword, request.Role);

        await _userRepository.AddAsync(
            user,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
