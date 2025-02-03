using Core.Common;
using UseCases.Abstractions.Messaging;

namespace UseCases.Users.Queries.GetUsers;

public record GetUsersQuery(
    string? SearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageSize) : IQuery<PagedList<UserResponse>>
{
}
