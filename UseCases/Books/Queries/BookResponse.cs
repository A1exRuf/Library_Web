using Core.Entities;

namespace UseCases.Books.Queries;

public record BookResponse(Guid Id, string Isbn, string Title,
        string Genree, string Description, Guid AuthorId, bool IsAvailable, Guid? ImageId);
