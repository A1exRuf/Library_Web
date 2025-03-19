using Core.Abstractions;
using Core.Exceptions;
using Mapster;
using UseCases.Abstractions.Messaging;

namespace UseCases.Authors.Queries.GetAuthorById;

internal sealed class GetAuthorQueryHandler : IQueryHandler<GetAuthorByIdQuery, AuthorResponse>
{
    private readonly IAuthorRepository _authorRepository;

    public GetAuthorQueryHandler(IAuthorRepository authorRepository) => _authorRepository = authorRepository;

    public async Task<AuthorResponse> Handle(
        GetAuthorByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetByIdAsync(request.AuthorId);

        if (author == null)
        {
            throw new AuthorNotFoundException(request.AuthorId);
        }

        var authorResponse = author.Adapt<AuthorResponse>();

        return authorResponse;
    }
}
