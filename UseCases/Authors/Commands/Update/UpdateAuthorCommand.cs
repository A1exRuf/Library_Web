using UseCases.Abstractions.Messaging;

namespace UseCases.Authors.Commands.UpdateAuthor;

public sealed record UpdateAuthorCommand(Guid AuthorId, string FirstName, string SecondName, DateTime DateOfBirth, string Country) : ICommand<bool>;
