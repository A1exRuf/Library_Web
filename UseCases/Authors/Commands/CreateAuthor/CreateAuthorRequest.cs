namespace UseCases.Authors.Commands.CreateAuthor;

public sealed record CreateAuthorRequest(string FirstName, 
    string SecondName, DateTime DateOfBirth, string Country);
