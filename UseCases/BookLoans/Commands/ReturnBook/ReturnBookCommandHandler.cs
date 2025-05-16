using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using UseCases.Abstractions.Messaging;

namespace UseCases.Books.Commands.ReturnBook;

public sealed class ReturnBookCommandHandler : ICommandHandler<ReturnBookCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<BookLoan> _bookLoanRepository;
    private readonly IRepository<Book> _bookRepository;

    public ReturnBookCommandHandler(
        IUnitOfWork unitOfWork,
        IRepository<BookLoan> bookLoanRepository,
        IRepository<Book> bookRepository)
    {
        _unitOfWork = unitOfWork;
        _bookLoanRepository = bookLoanRepository;
        _bookRepository = bookRepository;
    }

    public async Task<bool> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
    {
        // Getting a BookLoan
        var bookLoan = await _bookLoanRepository.GetAsync(
            x => x.Id == request.BookLoanId,
            asNoTracking: false,
            cancellationToken) ?? throw new BookLoanNotFoundException(request.BookLoanId);

        // Getting a borrowed Book
        var book = await _bookRepository.GetAsync(
            x => x.Id == bookLoan.BookId,
            asNoTracking: false,
            cancellationToken) ?? throw new BookNotFoundException(bookLoan.BookId);

        // Indicating that the book is available
        book.IsAvailable = true;
        _bookRepository.Update(book);

        // Removing BookLoan
        bool result = await _bookLoanRepository.RemoveByIdAsync(request.BookLoanId);

        if (!result)
            throw new BookLoanNotFoundException(request.BookLoanId);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
