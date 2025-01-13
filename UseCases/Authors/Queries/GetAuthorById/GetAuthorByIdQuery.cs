using UseCases.Abstractions.Messaging;

namespace UseCases.Authors.Queries.GetAuthorById;

public sealed record GetAuthorByIdQuery(Guid AuthorId) : IQuery<AuthorResponse>;
