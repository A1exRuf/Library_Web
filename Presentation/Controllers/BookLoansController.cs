using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UseCases.BookLoans.Commands.LoanBook;
using UseCases.BookLoans.Queries;
using UseCases.BookLoans.Queries.GetBookLoanById;
using UseCases.Books.Commands.LoanBook;
using UseCases.Books.Commands.ReturnBook;

namespace Presentation.Controllers;
public sealed class BookLoansController : ApiController
{
    [Authorize]
    [HttpGet("{bookLoanId}")]
    [ProducesResponseType(typeof(BookLoanResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBookLoanById(Guid bookLoanId, CancellationToken cancellationToken)
    {
        var query = new GetBookLoanByIdQuery(bookLoanId);

        var bookLoan = await Sender.Send(query, cancellationToken);

        return Ok(bookLoan);
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(BookLoanResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBookLoans(
        int page,
        int pageSize,
        CancellationToken cancellation)
    {
        var query = new GetBookLoansQuery(page, pageSize);

        var bookLoans = await Sender.Send(query, cancellation);

        return Ok(bookLoans);
    }

    [Authorize]
    [HttpPost]
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

    [Authorize]
    [HttpDelete("{bookLoanId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> ReturnBook(
        Guid bookLoanId,
        CancellationToken cancellationToken)
    {
        var command = new ReturnBookCommand(bookLoanId);

        bool succes = await Sender.Send(command, cancellationToken);

        if (!succes)
        {
            return NotFound();
        }

        return NoContent();
    }
}