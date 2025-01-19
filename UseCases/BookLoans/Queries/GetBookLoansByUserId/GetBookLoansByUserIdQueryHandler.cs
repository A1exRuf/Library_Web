using Core.Abstractions;
using Core.Entities;
using UseCases.Abstractions.Messaging;

namespace UseCases.BookLoans.Queries.GetBookLoanById;

internal sealed class GetBookLoansByUserIdQueryHandler : IQueryHandler<GetBookLoansByUserIdQuery, PagedList<BookLoanResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetBookLoansByUserIdQueryHandler(IApplicationDbContext context) => _context = context;

    public async Task<PagedList<BookLoanResponse>> Handle(GetBookLoansByUserIdQuery request, CancellationToken cancellationToken)
    {
        var bookLoanResponsesQuery = _context.BookLoans
            .Where(bl => bl.UserId == request.UserId)
            .OrderBy(bl => bl.Book.Title)
            .Select(bl => new BookLoanResponse(
                bl.Id,
                bl.UserId,
                bl.BookId,
                bl.LoanDate,
                bl.DueDate));

        var bookLoans = await PagedList<BookLoanResponse>.CreateAsync(bookLoanResponsesQuery,
            request.Page,
            request.PageSize);

        return bookLoans;
    }
}
