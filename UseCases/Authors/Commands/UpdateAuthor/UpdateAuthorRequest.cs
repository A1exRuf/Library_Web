namespace UseCases.Authors.Commands.UpdateAuthor;

public sealed record UpdateAuthorRequest(
    Guid AuthorId,
    string? FirstName,
    string? LastName,
    DateTime? DateOfBirth,
    string? Country);
