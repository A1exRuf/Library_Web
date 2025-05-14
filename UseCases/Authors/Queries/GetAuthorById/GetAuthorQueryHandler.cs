using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using UseCases.Abstractions.Messaging;

namespace UseCases.Authors.Queries.GetAuthorById;

internal sealed class GetAuthorQueryHandler : IQueryHandler<GetAuthorByIdQuery, AuthorResponse>
{
    private readonly IRepository<Author> _repository;

    public GetAuthorQueryHandler(IRepository<Author> repository) => _repository = repository;

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

        return response;
    }
}
