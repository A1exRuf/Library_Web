using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using UseCases.Abstractions.Messaging;

namespace UseCases.Books.Commands.CreateBook;

internal sealed class CreateBookCommandHandler : ICommandHandler<CreateBookCommand, Guid>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IBlobService _blobService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBookCommandHandler(IAuthorRepository authorRepository, IBookRepository bookRepository, IBlobService blobService, IUnitOfWork unitOfWork)
    {
        _authorRepository = authorRepository;
        _bookRepository = bookRepository;
        _blobService = blobService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetByIdAsync(request.AuthorId);

        if (author == null)
        {
            throw new AuthorNotFoundException(request.AuthorId);
        }

        string? imageUrl = null;

        Guid bookId = Guid.NewGuid();

        if (request.ImageStream != null)
        {
            string imageName = bookId + "_img";

            imageUrl = await _blobService.UploadAsync(request.ImageStream, imageName, "bimages");
        }

        var book = new Book(bookId, request.Isbn, request.Title, request.Genree, request.Description, request.AuthorId, author, imageUrl);

        _bookRepository.Add(book);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return book.Id;
    }
}
