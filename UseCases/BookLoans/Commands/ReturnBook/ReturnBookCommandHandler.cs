using Core.Abstractions;
using Core.Exceptions;
using UseCases.Abstractions.Messaging;

namespace UseCases.Books.Commands.ReturnBook;

public sealed class ReturnBookCommandHandler : ICommandHandler<ReturnBookCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBookLoanRepository _bookLoanRepository;
    private readonly IBookRepository _bookRepository;

    public ReturnBookCommandHandler(
        IUnitOfWork unitOfWork,
        IBookLoanRepository bookLoanRepository,
        IBookRepository bookRepository)
    {
        _unitOfWork = unitOfWork;
        _bookLoanRepository = bookLoanRepository;
        _bookRepository = bookRepository;
    }

    public async Task<bool> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
    {
        var bookLoan = await _bookLoanRepository.GetByIdAsync(request.BookLoanId);
        if (bookLoan == null)
        {
            throw new BookLoanNotFoundException(request.BookLoanId);
        }

        var book = await _bookRepository.GetByIdAsync(bookLoan.BookId);
        if (book == null)
        {
            throw new BookNotFoundException(bookLoan.BookId);
        }

        book.IsAvailable = true;

        _bookLoanRepository.Remove(bookLoan);
        _bookRepository.Update(book);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
