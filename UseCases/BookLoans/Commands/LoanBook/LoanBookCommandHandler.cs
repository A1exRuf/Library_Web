using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using UseCases.Abstractions.Messaging;
using UseCases.Exceptions;

namespace UseCases.Books.Commands.LoanBook;

public sealed class LoanBookCommandHandler : ICommandHandler<LoanBookCommand, Guid>
{
    private readonly IBookRepository _bookRepository;
    private readonly IUserRepository _userRepository;
    private readonly IBookLoanRepository _bookLoanRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public LoanBookCommandHandler(
        IBookRepository bookRepository,
        IUserRepository userRepository,
        IBookLoanRepository bookLoanRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserService currentUserService)
    {
        _bookRepository = bookRepository;
        _userRepository = userRepository;
        _bookLoanRepository = bookLoanRepository;
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(LoanBookCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _currentUserService.UserId ?? throw new AuthenticationException();

        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
        {
            throw new UserNotFoundException(userId);
        }

        var book = await _bookRepository.GetByIdAsync(request.BookId);

        if (book == null)
        {
            throw new BookNotFoundException(request.BookId);
        }
        else if (!book.IsAvailable)
        {
            throw new BookNotAvailableException(request.BookId);
        }

        var bookLoan = new BookLoan(Guid.NewGuid(), user, userId, book, request.BookId, DateTime.UtcNow);

        book.IsAvailable = false;
        
        _bookLoanRepository.Add(bookLoan);
        _bookRepository.Update(book);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return bookLoan.Id;
    }
}
