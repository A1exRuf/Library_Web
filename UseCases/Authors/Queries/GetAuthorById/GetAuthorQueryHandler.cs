using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using UseCases.Abstractions.Messaging;

namespace UseCases.Authors.Queries.GetAuthorById;

internal sealed class GetAuthorQueryHandler : IQueryHandler<GetAuthorByIdQuery, AuthorResponse>
{
    private readonly IRepository<Author> _repository;
    private readonly ILinkService _linkService;


    public GetAuthorQueryHandler(
        IRepository<Author> repository,
        ILinkService linkService)
    {
        _repository = repository;
        _linkService = linkService;
    }

    public async Task<AuthorResponse> Handle(
        GetAuthorByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var response = await _repository.GetAsync<AuthorResponse>(
            x => x.Id == request.AuthorId,
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
