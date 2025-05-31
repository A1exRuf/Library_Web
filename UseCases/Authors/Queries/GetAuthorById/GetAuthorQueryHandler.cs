using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using Core.Filters;
using UseCases.Abstractions.Messaging;

namespace UseCases.Authors.Queries.GetAuthorById;

internal sealed class GetAuthorQueryHandler : IQueryHandler<GetAuthorByIdQuery, AuthorResponse>
{
    private readonly IRepository<Author> _authorRepository;
    private readonly ILinkService _linkService;

    public GetAuthorQueryHandler(
        IRepository<Author> repository,
        ILinkService linkService)
    {
        _authorRepository = repository;
        _linkService = linkService;
    }

    public async Task<AuthorResponse> Handle(
        GetAuthorByIdQuery request, 
        CancellationToken cancellationToken)
    {
        AuthorFilter filter = new() { Id = request.AuthorId };

        var response = await _authorRepository.GetAsync<AuthorResponse>(
            filter,
            asNoTracking: true, 
            cancellationToken);

        if (response == null)
        {
            throw new AuthorNotFoundException(request.AuthorId);
        }

        AddLinksForAuthor(response);

        return response;
    }

    private void AddLinksForAuthor(AuthorResponse authorResponse)
    {
        authorResponse.Links.Add(
            _linkService.Generate(
                "GetAuthor",
                new { authorId = authorResponse.Id },
                "self",
                "GET"));

        authorResponse.Links.Add(
            _linkService.Generate(
                "PutAuthor",
                new { authorId = authorResponse.Id },
                "update-author",
                "PUT"));

        authorResponse.Links.Add(
            _linkService.Generate(
                "DeleteAuthor",
                new { authorId = authorResponse.Id },
                "delete-author",
                "DELETE"));
    }
}
