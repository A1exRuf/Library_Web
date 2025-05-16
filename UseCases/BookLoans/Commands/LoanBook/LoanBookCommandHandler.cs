using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using UseCases.Abstractions.Messaging;
using UseCases.Exceptions;

namespace UseCases.Books.Commands.LoanBook;

public sealed class LoanBookCommandHandler : ICommandHandler<LoanBookCommand, Guid>
{
    private readonly IRepository<Book> _bookRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<BookLoan> _bookLoanRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public LoanBookCommandHandler(
        IRepository<Book> bookRepository,
        IRepository<User> userRepository,
        IRepository<BookLoan> bookLoanRepository,
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
        // Getting an authorized user
        Guid userId = _currentUserService.UserId ?? throw new AuthenticationException();

        var user = await _userRepository.GetAsync(
            x => x.Id == userId,
            asNoTracking: false,
            cancellationToken) ?? throw 
            new UserNotFoundException(userId);

        // Getting a book
        var book = await _bookRepository.GetAsync(
            x => x.Id == x.Id,
            asNoTracking: false,
            cancellationToken) ?? throw 
            new BookNotFoundException(request.BookId);

        // Indicating that the book is occupied
        if (!book.IsAvailable)
            throw new BookNotAvailableException(request.BookId); 
        else 
            book.IsAvailable = false;

        _bookRepository.Update(book);

        // Creating a BookLoan
        var bookLoan = new BookLoan(Guid.NewGuid(), user, userId, book, request.BookId, DateTime.UtcNow);
        
        await _bookLoanRepository.AddAsync(
            bookLoan,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return bookLoan.Id;
    }
}
