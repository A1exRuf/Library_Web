using UseCases.Abstractions.Messaging;
using Core.Exceptions;
using System.Data;
using Dapper;

namespace UseCases.Authors.Queries.GetAuthorById;

internal sealed class GetAuthorQueryHandler : IQueryHandler<GetAuthorByIdQuery, AuthorResponse>
{
    private readonly IDbConnection _dbConnection;

    public GetAuthorQueryHandler(IDbConnection dbConnection) => _dbConnection = dbConnection;

    public async Task<AuthorResponse> Handle(
        GetAuthorByIdQuery request, 
        CancellationToken cancellationToken)
    {
        const string sql = @"SELECT * FROM ""Authors"" WHERE ""Id"" = @AuthorId";

        var author = await _dbConnection.QueryFirstOrDefaultAsync<AuthorResponse>(
            sql,
            new { request.AuthorId });

        if (author is null)
        {
            throw new AuthorNotFoundException(request.AuthorId);
        }    

        return author;
    }
}
