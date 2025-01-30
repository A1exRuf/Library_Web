using Core.Abstractions;
using MediatR;
using UseCases.Abstractions.Messaging;
using UseCases.Authors.Queries;
using UseCases.Books.Queries;

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
                new BookLoansBookDTO(
                    bl.Book.Id,
                    bl.Book.Isbn,
                    bl.Book.Title,
                    bl.Book.ImageUrl,
                    new BookLoansAuthorDTO(
                        bl.Book.Author.FirstName,
                        bl.Book.Author.LastName)),
                bl.LoanDate,
                bl.DueDate));

        var bookLoans = await PagedList<BookLoanResponse>.CreateAsync(bookLoanResponsesQuery,
            request.Page,
            request.PageSize);

        return bookLoans;
    }
}
