namespace UseCases.Authors.Queries;

public sealed record AuthorResponse(Guid Id, string FirstName,
    string SecondName, DateTime DateOfBirth, string Country);

