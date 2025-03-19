using Core.Abstractions;
using Core.Exceptions;
using Mapster;
using UseCases.Abstractions.Messaging;

namespace UseCases.Users.Queries.GetUserById;

internal sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository) => _userRepository = userRepository;

    public async Task<UserResponse> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user == null)
        {
            throw new UserNotFoundException(request.UserId);
        }

        var userResponse = user.Adapt<UserResponse>();

        return userResponse;
    }
}