using Core.Abstractions;
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
        var authors = await _authorRepository.GetAllAsync(a => new AuthorResponse(
            a.Id,
            a.FirstName,
            a.LastName,
            a.DateOfBirth,
            a.Country));

        return authors;
    }
}
