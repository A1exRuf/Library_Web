using Core.Abstractions;
using Core.Exceptions;
using UseCases.Abstractions.Messaging;

namespace UseCases.Books.Commands.UpdateBook;

public sealed class UpdateBookCommandHandler : ICommandHandler<UpdateBookCommand, bool>
{
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBookCommandHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.BookId);

        if (book == null)
        {
            throw new BookNotFoundException(request.BookId);
        }

        book.Isbn = request.Isbn;
        book.Title = request.Title;
        book.Genree = request.Genree;
        book.Description = request.Description;
        book.AuthorId = request.AuthorId;

        _bookRepository.Update(book);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}