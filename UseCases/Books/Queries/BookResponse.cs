namespace UseCases.Books.Queries;

public record BookResponse(
    Guid Id, 
    string Isbn,
    string Title,
    string Genree,
    string Description,
    BookAuthorDTO Author,
    bool IsAvailable,
    string? ImageUrl,
    List<LinkDTO> Links);

public record BookAuthorDTO(Guid Id, 
    string FirstName,
    string LastName, 
    DateTime DateOfBirth, 
    string Country);
