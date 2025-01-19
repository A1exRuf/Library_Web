using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UseCases.BookLoans.Commands.LoanBook;
using UseCases.BookLoans.Queries;
using UseCases.BookLoans.Queries.GetBookLoanById;
using UseCases.Books.Commands.LoanBook;
using UseCases.BookLoans.Commands.ReturnBook;
using UseCases.Books.Commands.ReturnBook;

namespace Presentation.Controllers;
public sealed class BookLoansController : ApiController
{
    [HttpGet("BookLoanById")]
    [ProducesResponseType(typeof(BookLoanResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBookLoan(Guid bookLoanId, CancellationToken cancellationToken)
    {
        var query = new GetBookLoanByIdQuery(bookLoanId);

        var bookLoan = await Sender.Send(query, cancellationToken);

        return Ok(bookLoan);
    }

    [HttpGet("BookLoansByUserId")]
    [ProducesResponseType(typeof(BookLoanResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBook(
        Guid userId,
        int page,
        int pageSize,
        CancellationToken cancellation)
    {
        var query = new GetBookLoansByUserIdQuery(userId, page, pageSize);

        var bookLoans = await Sender.Send(query, cancellation);

        return Ok(bookLoans);
    }

    [HttpPost("LoanBook")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> LoanBook(
        BookLoanRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.Adapt<LoanBookCommand>();

        var bookLoanId = await Sender.Send(command, cancellationToken);

        return CreatedAtAction(nameof(LoanBook), new { bookLoanId }, bookLoanId);
    }

    [HttpPost("ReturnBook")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> ReturnBook(
        ReturnBookRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.Adapt<ReturnBookCommand>();

        var bookLoanId = await Sender.Send(command, cancellationToken);

        return CreatedAtAction(nameof(ReturnBook), new { bookLoanId }, bookLoanId);
    }
}