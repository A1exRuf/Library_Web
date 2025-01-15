using Core.Exceptions;
using System.Data;
using UseCases.Abstractions.Messaging;
using Dapper;

namespace UseCases.Users.Queries.GetUserById;

internal sealed class GetUserQueryHandler : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    private readonly IDbConnection _dbConnection;

    public GetUserQueryHandler(IDbConnection dbConnection) => _dbConnection = dbConnection;

    public async Task<UserResponse> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        const string sql = @"SELECT * FROM ""Users"" WHERE ""Id"" = @UserId";

        var author = await _dbConnection.QueryFirstOrDefaultAsync<UserResponse>(
            sql,
            new { request.UserId });

        if (author is null)
        {
            throw new UserNotFoundException(request.UserId);
        }

        return author;
    }
}
