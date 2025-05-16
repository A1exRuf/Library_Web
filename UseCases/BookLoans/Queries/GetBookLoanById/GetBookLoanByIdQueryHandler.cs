using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using UseCases.Abstractions.Messaging;

namespace UseCases.BookLoans.Queries.GetBookLoanById;

internal sealed class GetBookLoanByIdQueryHandler : IQueryHandler<GetBookLoanByIdQuery, BookLoanResponse>
{
    private readonly IRepository<BookLoan> _repository;
    private readonly ILinkService _linkService;

    public GetBookLoanByIdQueryHandler(
        IRepository<BookLoan> repository,
        ILinkService linkService)
    {
        _repository = repository;
        _linkService = linkService;
    }

    public async Task<BookLoanResponse> Handle(
        GetBookLoanByIdQuery request,
        CancellationToken cancellationToken)
    {
        var response = await _repository.GetAsync<BookLoanResponse>(
            x => x.Id == request.BookLoanId,
            asNoTracking: true,
            cancellationToken) ?? throw 
            new BookLoanNotFoundException(request.BookLoanId);

        AddLinksForBookLoan(response);

        return response;
    }

    private void AddLinksForBookLoan(BookLoanResponse bookloanResponse)
    {
        bookloanResponse.Links.Add(
            _linkService.Generate(
                "GetBookLoan",
                new { bookLoanId = bookloanResponse.Id },
                "self",
                "GET"));

        bookloanResponse.Links.Add(
            _linkService.Generate(
                "ReturnBook",
                new { bookLoanId = bookloanResponse.Id },
                "return-book",
                "DELETE"));
    }
}
