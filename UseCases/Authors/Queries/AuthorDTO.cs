namespace UseCases.Authors.Queries;
public sealed record AuthorDTO(Guid Id, string FirstName,
    string LastName, DateTime DateOfBirth, string Country);
