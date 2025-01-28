using UseCases.Abstractions.Messaging;

namespace UseCases.Authors.Commands.UpdateAuthor;

public sealed record UpdateAuthorCommand(
    Guid AuthorId,
    string? FirstName, 
    string? LastName,
    DateTime? DateOfBirth,
    string? Country) : ICommand<bool>;
