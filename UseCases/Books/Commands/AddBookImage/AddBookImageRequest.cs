namespace UseCases.Books.Commands.AddBookImage;

public sealed record AddBookImageRequest(
    Guid BookId,
    Stream ImageStream,
    string ContentType);
