using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using UseCases.Abstractions.Messaging;

namespace UseCases.Books.Queries.GetBookById;

internal sealed class GetBookByIdQueryHandler : IQueryHandler<GetBookByIdQuery, BookResponse>
{
    private readonly IRepository<Book> _repository;
    private readonly ILinkService _linkService;

    public GetBookByIdQueryHandler(
        IRepository<Book> repository, 
        ILinkService linkService)
    {
        _repository = repository;
        _linkService = linkService;
    }

    public async Task<BookResponse> Handle(
        GetBookByIdQuery request,
        CancellationToken cancellationToken)
    {
        var response = await _repository.GetAsync<BookResponse>(
            x => x.Id == request.BookId,
            asNoTracking: true,
            cancellationToken);

        if (response == null)
        {
            throw new BookNotFoundException(request.BookId);
        }

        AddLinksForBook(response);

        return response;
    }

    private void AddLinksForBook(BookResponse bookResponse)
    {
        bookResponse.Links.Add(
            _linkService.Generate(
                "GetBook", 
                new { bookId = bookResponse.Id },
                "self",
                "GET"));

        bookResponse.Links.Add(
            _linkService.Generate(
                "TakeBook",
                new { bookId = bookResponse.Id },
                "take-book",
                "POST"));

        bookResponse.Links.Add(
            _linkService.Generate(
                "GetAuthor", 
                new { authorId = bookResponse.Author.Id },
                "author",
                "GET"));

        bookResponse.Links.Add(
            _linkService.Generate(
                "PutBook", 
                new { bookId = bookResponse.Id },
                "update-book",
                "PUT"));

        bookResponse.Links.Add(
            _linkService.Generate(
                "DeleteBook", 
                new { bookId = bookResponse.Id },
                "delete-book",
                "DELETE"));
    }
}
