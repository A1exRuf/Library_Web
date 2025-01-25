using Core.Abstractions;
using Core.Exceptions;
using UseCases.Abstractions.Messaging;

namespace UseCases.Books.Commands.DeleteBook;

internal sealed class DeleteBookCommandHandler : ICommandHandler<DeleteBookCommand, bool>
{
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBlobService _blobService;

    public DeleteBookCommandHandler(IBookRepository bookRepository, IBlobService blobService, IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _blobService = blobService;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.BookId);

        if (book == null)
        {
            throw new BookNotFoundException(request.BookId);
        }

        if (book.ImageUrl != null)
        {
            await _blobService.DeleteAsync(book.ImageUrl);
        }

        _bookRepository.Remove(book);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
