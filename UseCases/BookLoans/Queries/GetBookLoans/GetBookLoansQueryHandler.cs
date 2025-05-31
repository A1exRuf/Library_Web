using Core.Abstractions;
using Core.Entities;
using Core.Filters;
using UseCases.Abstractions.Messaging;
using UseCases.Exceptions;

namespace UseCases.BookLoans.Queries.GetBookLoans;

internal sealed class GetBookLoansQueryHandler : IQueryHandler<GetBookLoansQuery, List<BookLoanResponse>>
{
    private readonly IRepository<BookLoan> _bookLoanRepository;

    private readonly ICurrentUserService _currentUserService;

    public GetBookLoansQueryHandler(
        IRepository<BookLoan> bookLoanRepository,
        ICurrentUserService currentUserService)
    {
        _bookLoanRepository = bookLoanRepository;
        _currentUserService = currentUserService;
    }

    public async Task<List<BookLoanResponse>> Handle(GetBookLoansQuery request, CancellationToken cancellationToken)
    {
        // Getting authenticated user
        var userId = _currentUserService.UserId
            ?? throw new AuthenticationException();

        // Getting list of BookLoans
        BookLoanFilter filter = new() { UserId = userId };

        var bookLoans = await _bookLoanRepository.GetListAsync<BookLoanResponse>(
            filter,
            asNoTracking: true,
            cancellationToken: cancellationToken);

        return bookLoans;
    }
}