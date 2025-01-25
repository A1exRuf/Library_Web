using Core.Abstractions;
using UseCases.Authors.Queries;

namespace UseCases.Books.Queries;

public record BookResponse(
    Guid Id, 
    string Isbn,
    string Title,
    string Genree,
    string Description,
    AuthorDTO Author,
    bool IsAvailable,
    string ImageUrl);

