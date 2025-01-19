using Core.Abstractions;
using Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using UseCases.Abstractions.Messaging;

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
                bl.UserId,
                bl.BookId,
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
