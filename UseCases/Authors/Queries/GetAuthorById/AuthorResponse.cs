namespace UseCases.Authors.Queries.GetAuthorById;

public sealed record AuthorResponse(Guid Id, string FirstName, 
    string SecondName, DateTime DateOfBirth, string Country);

