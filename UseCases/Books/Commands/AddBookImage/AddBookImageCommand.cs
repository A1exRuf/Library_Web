using UseCases.Abstractions.Messaging;

namespace UseCases.Books.Commands.AddBookImage;

public sealed record AddBookImageCommand(Guid BookId, Stream ImageStream, string ContentType) : ICommand<Guid>;
