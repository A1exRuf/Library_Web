using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using UseCases.Abstractions.Messaging;

namespace UseCases.BookLoans.Queries.GetBookLoanById;

internal sealed class GetBookLoanByIdQueryHandler : IQueryHandler<GetBookLoanByIdQuery, BookLoanResponse>
{
    private readonly IBookLoanRepository _bookLoanRepository;

    public GetBookLoanByIdQueryHandler(IBookLoanRepository bookLoanRepository) =>
        _bookLoanRepository = bookLoanRepository;

    public async Task<BookLoanResponse> Handle(
        GetBookLoanByIdQuery request,
        CancellationToken cancellationToken)
    {
        BookLoan? bookLoan = await _bookLoanRepository.GetByIdAsync(request.BookLoanId);

        if (bookLoan == null)
        {
            throw new BookLoanNotFoundException(request.BookLoanId);
        }

        BookLoanResponse bookLoanResponse = new(
            bookLoan.Id,
            new BookLoansBookDTO(
                bookLoan.Book.Id,
                bookLoan.Book.Isbn,
                bookLoan.Book.Title,
                bookLoan.Book.ImageUrl,
                new BookLoansAuthorDTO(
                    bookLoan.Book.Author.FirstName,
                    bookLoan.Book.Author.LastName
                )),
            bookLoan.LoanDate,
            bookLoan.DueDate);

        return bookLoanResponse;
    }
}
