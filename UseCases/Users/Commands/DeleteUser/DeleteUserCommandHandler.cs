using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using UseCases.Abstractions.Messaging;

namespace UseCases.Users.Commands.DeleteUser;

internal sealed class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand, bool>
{
    private readonly IRepository<User> _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(
        IRepository<User> userRepository, 
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _userRepository.RemoveByIdAsync(
            request.UserId, 
            cancellationToken);

        if (!result)
            throw new UserNotFoundException(request.UserId);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}