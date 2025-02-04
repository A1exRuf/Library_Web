using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using Moq;
using UseCases.Books.Queries.GetBookById;

namespace UseCases.UnitTests.Books.Queries;

public class GetBookByIdQueryHandlerTests
{
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly GetBookByIdQueryHandler _handler;

    public GetBookByIdQueryHandlerTests()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();
        _handler = new GetBookByIdQueryHandler(
            _bookRepositoryMock.Object);
    }

    private Author author = new(
            Guid.NewGuid(),
            "FirstName",
            "SecondName",
            DateTime.UtcNow,
            "Country");

    [Fact]
    public async Task Handle_Should_ReturnBookResponse_WhenExist()
    {
        //Arrange
        var bookId = Guid.NewGuid();
        string imageUrl = $"http://127.0.0.1:10000/devstoreaccount1/bimages/{bookId}_img";
        var book = new Book(
            bookId,
            "00000000000",
            "title",
            "Fiction",
            "description",
            author.Id,
            author,
            imageUrl);

        _bookRepositoryMock.Setup(repo => repo.GetByIdAsync(bookId))
            .ReturnsAsync(book);

        var query = new GetBookByIdQuery(bookId);

        //Act
        var result = await _handler.Handle(query, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(book.Id, result.Id);
        Assert.Equal(book.Isbn, result.Isbn);
        Assert.Equal(book.Title, result.Title);
        Assert.Equal(book.Genree, result.Genree);
        Assert.Equal(book.Description, result.Description);
        Assert.Equal(book.AuthorId, result.Author.Id);
        Assert.Equal(book.IsAvailable, result.IsAvailable);
        Assert.Equal(book.ImageUrl, result.ImageUrl);
    }

    [Fact]
    public async Task Handle_Should_ThrowException_WhenNotExist()
    {
        //Arrange
        var bookId = Guid.NewGuid();
        _bookRepositoryMock.Setup(_bookRepositoryMock => _bookRepositoryMock.GetByIdAsync(bookId))
            .ReturnsAsync((Book?)null);

        var query = new GetBookByIdQuery(bookId);

        //Act & Assert
        await Assert.ThrowsAsync<BookNotFoundException>(async () => await _handler.Handle(query, CancellationToken.None));
    }
}
