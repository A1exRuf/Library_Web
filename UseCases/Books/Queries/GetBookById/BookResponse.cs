using Core.Entities;

namespace UseCases.Books.Queries.GetBookById;

public sealed record BookResponse(Guid Id, string Isbn, string Title,
        string Genree, string Description, Guid AuthorId, DateTime TakenAt, Guid? ImageId);
