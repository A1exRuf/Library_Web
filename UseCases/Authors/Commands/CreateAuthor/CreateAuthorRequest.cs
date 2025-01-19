namespace UseCases.Authors.Commands.CreateAuthor;

public sealed record CreateAuthorRequest(string FirstName, 
    string LastName, DateTime DateOfBirth, string Country);
