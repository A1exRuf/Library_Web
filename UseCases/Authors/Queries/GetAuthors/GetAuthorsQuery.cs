using UseCases.Abstractions.Messaging;

namespace UseCases.Authors.Queries.GetAuthors;

public sealed record GetAuthorsQuery : IQuery<List<AuthorResponse>>;
