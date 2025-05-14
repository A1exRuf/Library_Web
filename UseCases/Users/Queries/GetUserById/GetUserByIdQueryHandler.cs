using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using UseCases.Abstractions.Messaging;

namespace UseCases.Users.Queries.GetUserById;

internal sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    private readonly IRepository<User> _repository;

    public GetUserByIdQueryHandler(IRepository<User> repository) => _repository = repository;

    public async Task<UserResponse> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        var response = await _repository.GetAsync<UserResponse>(
            x => x.Id == request.UserId,
            asNoTracking: true,
            cancellationToken);

        if (response == null)
        {
            throw new UserNotFoundException(request.UserId);
        }

        return response;
    }
}