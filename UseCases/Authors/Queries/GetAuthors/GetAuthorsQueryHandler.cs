using Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using UseCases.Abstractions.Messaging;
using UseCases.Authors.Queries.GetAuthors;

namespace UseCases.Authors.Queries.GetAuthorById;

internal sealed class GetAuthorsQueryHandler : IQueryHandler<GetAuthorsQuery, List<AuthorResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetAuthorsQueryHandler(IApplicationDbContext context) => _context = context;

    public async Task<List<AuthorResponse>> Handle(
        GetAuthorsQuery request,
        CancellationToken cancellationToken)
    {
        var authors = await _context
            .Authors
            .Select(a => new AuthorResponse(
                a.Id,
                a.FirstName,
                a.LastName,
                a.DateOfBirth,
                a.Country))
            .ToListAsync(cancellationToken);

        return authors;
    }
}
