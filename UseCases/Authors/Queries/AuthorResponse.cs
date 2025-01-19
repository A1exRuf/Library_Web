namespace UseCases.Authors.Queries;

public sealed record AuthorResponse(Guid Id, string FirstName,
    string LastName, DateTime DateOfBirth, string Country);

