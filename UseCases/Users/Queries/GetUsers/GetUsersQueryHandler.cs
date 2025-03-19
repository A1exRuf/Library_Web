using Core.Abstractions;
using Core.Common;
using Core.Entities;
using Mapster;
using System.Linq.Expressions;
using UseCases.Abstractions.Messaging;

namespace UseCases.Users.Queries.GetUsers;

public sealed class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, PagedList<UserResponse>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersQueryHandler(IUserRepository userRepository) => _userRepository = userRepository;

    public async Task<PagedList<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        IQueryable<User> usersQuery = _userRepository.Query();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            usersQuery = usersQuery.Where(b =>
            b.Name.Contains(request.SearchTerm) ||
            b.Email.ToLower().Contains(request.SearchTerm.ToLower()));
        }

        if (request.SortOrder?.ToLower() == "desc")
        {
            usersQuery = usersQuery.OrderByDescending(GetSortProperty(request));
        }
        else
        {
            usersQuery = usersQuery.OrderBy(GetSortProperty(request));
        }

        var userResponsesQuery = usersQuery
            .Select(u => u.Adapt<UserResponse>());

        var users = await _userRepository.GetPagedAsync(userResponsesQuery, request.Page, request.PageSize);

        return users;
    }

    private static Expression<Func<User, object>> GetSortProperty(GetUsersQuery request)
    {
        return request.SortColumn?.ToLower() switch
        {
            "name" => b => b.Name,
            "email" => b => b.Email,
            _ => b => b.Id
        };
    }
}
