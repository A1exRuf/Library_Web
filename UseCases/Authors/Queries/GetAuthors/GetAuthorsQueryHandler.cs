using Core.Abstractions;
using Core.Entities;
using Core.Filters;
using UseCases.Abstractions.Messaging;
using UseCases.Authors.Queries.GetAuthors;

namespace UseCases.Authors.Queries.GetAuthorById;

internal sealed class GetAuthorsQueryHandler : IQueryHandler<GetAuthorsQuery, List<AuthorResponse>>
{
    private readonly IRepository<Author> _authorRepository;

    public GetAuthorsQueryHandler(IRepository<Author> authorRepository) => _authorRepository = authorRepository;

    public async Task<List<AuthorResponse>> Handle(
        GetAuthorsQuery request,
        CancellationToken cancellationToken)
    {
        AuthorFilter filter = new() { };

        var authors = await _authorRepository.GetListAsync<AuthorResponse>(
            filter, 
            asNoTracking: true, 
            cancellationToken: cancellationToken);

        return authors;
    }
}
