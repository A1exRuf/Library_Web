using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using UseCases.Abstractions.Messaging;

namespace UseCases.Users.Queries.GetUserById;

internal sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    private readonly IRepository<User> _repository;
    private readonly ILinkService _linkService;

    public GetUserByIdQueryHandler(
        IRepository<User> repository,
        ILinkService linkService)
    {
        _repository = repository;
        _linkService = linkService;
    }

    public async Task<UserResponse> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        var response = await _repository.GetAsync<UserResponse>(
            x => x.Id == request.UserId,
            asNoTracking: true,
            cancellationToken) ?? throw 
            new UserNotFoundException(request.UserId);

        AddLinksForUser(response);

        return response;
    }

    private void AddLinksForUser(UserResponse userResponse)
    {
        userResponse.Links.Add(
            _linkService.Generate(
                "GetUser",
                new { userId = userResponse.Id },
                "self",
                "GET"));

        userResponse.Links.Add(
            _linkService.Generate(
                "DeleteUser",
                new { userId = userResponse.Id },
                "delete-user",
                "DELETE"));

        userResponse.Links.Add(
            _linkService.Generate(
                "UpdateUser",
                new { userId = userResponse.Id },
                "update-user",
                "PUT"));
    }
}