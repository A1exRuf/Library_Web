using Core.Abstractions;
using UseCases.Abstractions.Messaging;

namespace UseCases.Books.Queries.GetImageByItsId;

public sealed record GetImageByItsIdQuery(Guid ImageId) : IQuery<ImageResponse>;
