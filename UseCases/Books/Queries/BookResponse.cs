using Core.Abstractions;

namespace UseCases.Books.Queries;

public record BookResponse(
    Guid Id, 
    string Isbn,
    string Title,
    string Genree,
    string Description,
    Guid AuthorId,
    string AuthorFirstName,
    string AuthorLastName,
    DateTime AuthorDateOfBirth,
    string AuthorCountry,
    bool IsAvailable,
    Guid? ImageId);

