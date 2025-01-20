using UseCases.Abstractions.Messaging;

namespace UseCases.Authors.Commands.CreateAuthor;

public sealed record CreateAuthorCommand(
    string FirstName, 
    string LastName, 
    DateTime DateOfBirth, 
    string Country) : ICommand<Guid>;
