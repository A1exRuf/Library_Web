using Core.Abstractions;
using UseCases.Abstractions.Messaging;

namespace UseCases.Books.Commands.DeleteBook;

internal sealed class DeleteBookCommandHandler : ICommandHandler<DeleteBookCommand, bool>
{
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBookCommandHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.BookId, cancellationToken);

        if (book == null)
        {
            return false;
        }

        _bookRepository.Delete(book);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
