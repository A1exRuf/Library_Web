using Core.Abstractions;
using Mapster;
using UseCases.Abstractions.Messaging;
using UseCases.Authors.Queries.GetAuthors;

namespace UseCases.Authors.Queries.GetAuthorById;

internal sealed class GetAuthorsQueryHandler : IQueryHandler<GetAuthorsQuery, List<AuthorResponse>>
{
    private readonly IAuthorRepository _authorRepository;

    public GetAuthorsQueryHandler(IAuthorRepository authorRepository) => _authorRepository = authorRepository;

    public async Task<List<AuthorResponse>> Handle(
        GetAuthorsQuery request,
        CancellationToken cancellationToken)
    {
        var authors = await _authorRepository.GetAllAsync(
            a => a.Adapt<AuthorResponse>());

        return authors;
    }
}
