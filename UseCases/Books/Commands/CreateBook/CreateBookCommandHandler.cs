using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using UseCases.Abstractions.Messaging;

namespace UseCases.Books.Commands.CreateBook;

internal sealed class CreateBookCommandHandler : ICommandHandler<CreateBookCommand, Guid>
{
    private readonly IRepository<Author> _authorRepository;
    private readonly IRepository<Book> _bookRepository;
    private readonly IBlobService _blobService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBookCommandHandler(
        IRepository<Author> authorRepository, 
        IRepository<Book> bookRepository, 
        IBlobService blobService, 
        IUnitOfWork unitOfWork)
    {
        _authorRepository = authorRepository;
        _bookRepository = bookRepository;
        _blobService = blobService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        // Getting author of the book
        var author = await _authorRepository.GetAsync(
            x => x.Id == request.AuthorId,
            asNoTracking: false,
            cancellationToken) ?? throw 
            new AuthorNotFoundException(request.AuthorId);

        // Initializing book ID
        Guid bookId = Guid.NewGuid();

        // Uploading Image and getting it's URL
        string? imageUrl = null;

        if (request.ImageStream != null)
        {
            using (request.ImageStream)
            {
                string imageName = bookId + "_img";
                imageUrl = await _blobService.
                    UploadAsync(request.ImageStream, imageName, "bimages");
            }
        }

        // Creating Book
        var book = new Book(
            bookId, 
            request.Isbn, 
            request.Title, 
            request.Genree, 
            request.Description, 
            request.AuthorId, 
            author, 
            imageUrl);

        await _bookRepository.AddAsync(book, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return book.Id;
    }
}
