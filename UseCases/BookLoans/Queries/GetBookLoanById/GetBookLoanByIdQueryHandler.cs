using Core.Abstractions;
using Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using UseCases.Abstractions.Messaging;
using UseCases.Authors.Queries;
using UseCases.Books.Queries;

namespace UseCases.BookLoans.Queries.GetBookLoanById;

internal sealed class GetBookLoanByIdQueryHandler : IQueryHandler<GetBookLoanByIdQuery, BookLoanResponse>
{
    private readonly IApplicationDbContext _context;

    public GetBookLoanByIdQueryHandler(IApplicationDbContext context) => _context = context;

    public async Task<BookLoanResponse> Handle(
        GetBookLoanByIdQuery request,
        CancellationToken cancellationToken)
    {
        var bookLoan = await _context
            .BookLoans
            .Where(bl => bl.Id == request.BookLoanId)
            .Select(bl => new BookLoanResponse(
                bl.Id,
                new BookLoansBookDTO(
                    bl.Book.Id,
                    bl.Book.Isbn,
                    bl.Book.Title,
                    bl.Book.ImageUrl,
                    new BookLoansAuthorDTO(
                        bl.Book.Author.FirstName,
                        bl.Book.Author.LastName)),
                bl.LoanDate,
                bl.DueDate))
            .FirstOrDefaultAsync(cancellationToken);

        if (bookLoan == null)
        {
            throw new BookLoanNotFoundException(request.BookLoanId);
        }

        return bookLoan;
    }
}
