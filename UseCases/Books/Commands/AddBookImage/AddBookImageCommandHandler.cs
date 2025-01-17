using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using System.Data;
using Dapper;
using UseCases.Abstractions.Messaging;

namespace UseCases.Books.Commands.AddBookImage;

internal sealed class AddBookImageCommandHandler : ICommandHandler<AddBookImageCommand, Guid>
{
    private readonly IBookRepository _bookRepository;
    private readonly IBlobService _blobService;
    private readonly IUnitOfWork _unitOfWork;

    public AddBookImageCommandHandler(
        IBookRepository bookRepository,
        IBlobService blobService,
        IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _blobService = blobService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(AddBookImageCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.BookId, cancellationToken);
        if (book is null)
        {
            throw new BookNotFoundException(request.BookId);
        }

        if (book.ImageId.HasValue)
        {
            await _blobService.DeleteAsync(book.ImageId.Value);
        }

        var imageId = await _blobService.UploadAsync(request.ImageStream, request.ContentType, cancellationToken);

        book.ImageId = imageId;

        _bookRepository.Update(book);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return imageId;
    }
}