using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using UseCases.Abstractions.Messaging;

namespace UseCases.BookLoans.Queries.GetBookLoanById;

internal sealed class GetBookLoanByIdQueryHandler : IQueryHandler<GetBookLoanByIdQuery, BookLoanResponse>
{
    private readonly IRepository<BookLoan> _repository;

    public GetBookLoanByIdQueryHandler(IRepository<BookLoan> repository) =>
        _repository = repository;

    public async Task<BookLoanResponse> Handle(
        GetBookLoanByIdQuery request,
        CancellationToken cancellationToken)
    {
        var response = await _repository.GetAsync<BookLoanResponse>(
            x => x.Id == request.BookLoanId,
            asNoTracking: true,
            cancellationToken);

        if (response == null)
        {
            throw new BookLoanNotFoundException(request.BookLoanId);
        }

        return response;
    }
}
