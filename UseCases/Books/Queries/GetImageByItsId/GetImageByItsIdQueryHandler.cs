using Core.Abstractions;
using UseCases.Abstractions.Messaging;

namespace UseCases.Books.Queries.GetImageByItsId;

public sealed class GetImageByItsIdQueryHandler : IQueryHandler<GetImageByItsIdQuery, ImageResponse>
{
    private readonly IBlobService _blobService;

    public GetImageByItsIdQueryHandler(IBlobService blobService)
        {
        _blobService = blobService;
        }
    public async Task<ImageResponse> Handle(GetImageByItsIdQuery request, CancellationToken cancellationToken)
    {
        FileResponse fileResponse = await _blobService.DownloadAsync(request.ImageId, cancellationToken);

        return new ImageResponse(fileResponse);
    }
}