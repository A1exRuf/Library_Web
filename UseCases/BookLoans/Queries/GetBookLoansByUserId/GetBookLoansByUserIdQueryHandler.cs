using Core.Abstractions;
using Core.Common;
using Core.Exceptions;
using UseCases.Abstractions.Messaging;

namespace UseCases.BookLoans.Queries.GetBookLoanById;

internal sealed class GetBookLoansByUserIdQueryHandler : IQueryHandler<GetBookLoansByUserIdQuery, PagedList<BookLoanResponse>>
{
    private readonly IBookLoanRepository _bookLoanRepository;

    public GetBookLoansByUserIdQueryHandler(IBookLoanRepository bookLoanRepository) => _bookLoanRepository = bookLoanRepository;

    public async Task<PagedList<BookLoanResponse>> Handle(GetBookLoansByUserIdQuery request, CancellationToken cancellationToken)
    {
        var bookLoans = await _bookLoanRepository.GetByUserIdAsync(
            request.UserId, 
            bl => new BookLoanResponse(
                bl.Id,
                new BookLoansBookDTO(
                    bl.Book.Id,
                    bl.Book.Isbn,
                    bl.Book.Title,
                    bl.Book.ImageUrl,
                    new BookLoansAuthorDTO(
                        bl.Book.Author.FirstName,
                        bl.Book.Author.LastName
                    )),
                bl.LoanDate,
                bl.DueDate),

            request.Page, request.PageSize);

        if(bookLoans == null)
        {
            throw new UserNotFoundException(request.UserId);
        }

        return bookLoans;
    }
}
