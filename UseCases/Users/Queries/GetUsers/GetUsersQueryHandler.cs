using Core.Abstractions;
using Core.Common;
using Core.Entities;
using Core.Filters;
using UseCases.Abstractions.Messaging;

namespace UseCases.Users.Queries.GetUsers;

public sealed class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, PagedList<UserResponse>>
{
    private readonly IRepository<User> _userRepository;

    public GetUsersQueryHandler(IRepository<User> userRepository) => _userRepository = userRepository;

    public async Task<PagedList<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var filter = new UsersFilter { SearchTerm = request.SearchTerm };

        var response = await _userRepository.GetPagedListAsync<UserResponse, UsersFilter>(
            request.Page,
            request.PageSize,
            filter,
            x => x.Name,
            cancellationToken: cancellationToken);

        return response;
    }
}
