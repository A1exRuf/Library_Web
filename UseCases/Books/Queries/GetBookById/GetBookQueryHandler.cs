using Core.Exceptions;
using System.Data;
using UseCases.Abstractions.Messaging;
using Dapper;

namespace UseCases.Books.Queries.GetBookById;

internal sealed class GetBookQueryHandler : IQueryHandler<GetBookByIdQuery, BookResponse>
{
    private readonly IDbConnection _dbConnection;

    public GetBookQueryHandler(IDbConnection dbConnection) => _dbConnection = dbConnection;

    public async Task<BookResponse> Handle(
        GetBookByIdQuery request,
        CancellationToken cancellationToken)
    {
        const string sql = @"SELECT * FROM ""Books"" WHERE ""Id"" = @BookId";

        var book = await _dbConnection.QueryFirstOrDefaultAsync<BookResponse>(
            sql,
            new { request.BookId });

        if (book is null)
        {
            throw new AuthorNotFoundException(request.BookId);
        }

        return book;
    }
}
