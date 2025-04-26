using Core.Abstractions;
using Core.Common;
using UseCases.Abstractions.Messaging;
using UseCases.Exceptions;

namespace UseCases.BookLoans.Queries.GetBookLoanById;

internal sealed class GetBookLoansQueryHandler : IQueryHandler<GetBookLoansQuery, PagedList<BookLoanResponse>>
{
    private readonly IBookLoanRepository _bookLoanRepository;

    private readonly ICurrentUserService _currentUserService;

    public GetBookLoansQueryHandler(
        IBookLoanRepository bookLoanRepository,
        ICurrentUserService currentUserService)
    {
        _bookLoanRepository = bookLoanRepository;
        _currentUserService = currentUserService;
    }

    public async Task<PagedList<BookLoanResponse>> Handle(GetBookLoansQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId
            ?? throw new AuthenticationException();

        var bookLoans = await _bookLoanRepository.GetByUserIdAsync<BookLoanResponse>(
            userId, 
            request.Page, 
            request.PageSize);

        return bookLoans;
    }
}
