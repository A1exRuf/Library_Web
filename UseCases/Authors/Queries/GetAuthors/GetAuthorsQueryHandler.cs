using Core.Abstractions;
using Core.Entities;
using Mapster;
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
        var authors = await _authorRepository.GetListAsync<AuthorResponse>(
            null, 
            asNoTracking: true, 
            cancellationToken: cancellationToken);

        return authors;
    }
}
