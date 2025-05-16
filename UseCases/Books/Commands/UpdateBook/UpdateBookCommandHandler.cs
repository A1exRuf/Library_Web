using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using UseCases.Abstractions.Messaging;

namespace UseCases.Books.Commands.UpdateBook;

public sealed class UpdateBookCommandHandler : ICommandHandler<UpdateBookCommand, bool>
{
    private readonly IRepository<Book> _bookRepository;
    private readonly IBlobService _blobService;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBookCommandHandler(
        IRepository<Book> bookRepository, 
        IBlobService blobService,
        IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _blobService = blobService;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        // Getting book
        var book = await _bookRepository.GetAsync(
            x => x.Id == request.BookId,
            asNoTracking: false,
            cancellationToken) ?? throw new BookNotFoundException(request.BookId);

        // Updating image
        string? imageUrl = null;

        if (request.ImageStream != null)
        {
            using (request.ImageStream)
            {
                string imageName = request.BookId + "_img";
                imageUrl = await _blobService.UploadAsync(request.ImageStream, imageName, "bimages");
            } 
        }

        // Updating book
        book.Isbn = request.Isbn ?? book.Isbn;
        book.Title = request.Title ?? book.Title;
        book.Genree = request.Genree ?? book.Genree;
        book.Description = request.Description ?? book.Description;
        book.AuthorId = request.AuthorId ?? book.AuthorId;
        book.ImageUrl = imageUrl;

        _bookRepository.Update(book);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}