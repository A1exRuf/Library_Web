using Core.Entities;

namespace UseCases.Books.Queries;

public record BookResponse(Guid Id, string Isbn, string Title,
        string Genree, string Description, Guid AuthorId, DateTime? TakenAt, Guid? ImageId);
