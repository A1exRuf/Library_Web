using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using Core.Filters;
using UseCases.Abstractions.Messaging;

namespace UseCases.Books.Commands.DeleteBook;

internal sealed class DeleteBookCommandHandler : ICommandHandler<DeleteBookCommand, bool>
{
    private readonly IRepository<Book> _bookRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBlobService _blobService;

    public DeleteBookCommandHandler(
        IRepository<Book> bookRepository, 
        IBlobService blobService, 
        IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _blobService = blobService;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        // Getting Image IRL
        var book = await _bookRepository.GetAsync<DeleteBookDTO>(
            new BooksFilter { Id = request.BookId },
            asNoTracking: false,
            cancellationToken) ?? throw 
            new BookNotFoundException(request.BookId);

        // Removing book from dbContext
        bool result = await _bookRepository.RemoveByIdAsync(request.BookId, cancellationToken);

        // Removing image
        if (!result)
            return false;
        else if (book.ImageUrl != null)
            await _blobService.DeleteAsync(book.ImageUrl);

        // Commiting changes
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}