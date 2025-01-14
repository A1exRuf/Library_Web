using UseCases.Abstractions.Messaging;

namespace UseCases.Authors.Commands.DeleteAuthor;

public sealed record DeleteAuthorCommand(Guid AuthorId) : ICommand<bool>;
