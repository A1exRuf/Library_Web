using UseCases.Abstractions.Messaging;

namespace UseCases.Authors.Commands.CreateAuthor;

public sealed record CreateAuthorCommand(string FirstName, string SecondName, DateTime DateOfBirth, string Country) : ICommand<Guid>;
