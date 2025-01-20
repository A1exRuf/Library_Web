using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using UseCases.Abstractions.Messaging;

namespace UseCases.Books.Commands.LoanBook;

public sealed class LoanBookCommandHandler : ICommandHandler<LoanBookCommand, Guid>
{
    private readonly IBookRepository _bookRepository;
    private readonly IUserRepository _userRepository;
    private readonly IBookLoanRepository _bookLoanRepository;
    private readonly IUnitOfWork _unitOfWork;

    public LoanBookCommandHandler(
        IBookRepository bookRepository,
        IUserRepository userRepository,
        IBookLoanRepository bookLoanRepository,
        IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _userRepository = userRepository;
        _bookLoanRepository = bookLoanRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(LoanBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.BookId);

        if (book == null)
        {
            throw new BookNotFoundException(request.BookId);
        }
        else if (!book.IsAvailable)
        {
            throw new BookNotAvailableException(request.BookId);
        }

        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user == null)
        {
            throw new UserNotFoundException(request.UserId);
        }

        var bookLoan = new BookLoan(Guid.NewGuid(), user, request.UserId, book, request.BookId, DateTime.UtcNow);

        book.IsAvailable = false;

        _bookLoanRepository.Add(bookLoan);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return bookLoan.Id;
    }
}
