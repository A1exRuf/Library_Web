using Core.Abstractions;
using Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using UseCases.Abstractions.Messaging;

namespace UseCases.Authors.Queries.GetAuthorById;

internal sealed class GetAuthorQueryHandler : IQueryHandler<GetAuthorByIdQuery, AuthorResponse>
{
    private readonly IApplicationDbContext _context;

    public GetAuthorQueryHandler(IApplicationDbContext context) => _context = context;

    public async Task<AuthorResponse> Handle(
        GetAuthorByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var author = await _context
            .Authors
            .Where(a => a.Id == request.AuthorId)
            .Select(a => new AuthorResponse(
                a.Id,
                a.FirstName,
                a.SecondName,
                a.DateOfBirth,
                a.Country))
            .FirstOrDefaultAsync(cancellationToken);

        if (author is null)
        {
            throw new AuthorNotFoundException(request.AuthorId);
        }    

        return author;
    }
}
