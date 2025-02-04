using Core.Abstractions;
using Core.Common;
using Core.Entities;
using FluentAssertions;
using Moq;
using UseCases.Books.Queries;
using UseCases.Books.Queries.GetBooks;

namespace UseCases.UnitTests.Books.Queries;

public class GetBooksQueryHandlerTests
{
    private readonly List<Book> _books;
    private readonly Mock<IBookRepository> _repositoryMock;
    private readonly GetBooksQueryHandler _handler;

    public GetBooksQueryHandlerTests()
    {
        _books = GetTestBooks();
        _repositoryMock = SetupRepositoryMock(_books);
        _handler = new GetBooksQueryHandler(_repositoryMock.Object);
    }

    private List<Book> GetTestBooks()
    {
        var author1 = new Author(
            Guid.NewGuid(),
            "John",
            "Doe",
            new DateTime(1970, 1, 1),
            "USA");

        var author2 = new Author(
            Guid.NewGuid(),
            "Jane",
            "Smith",
            new DateTime(1980, 5, 15),
            "UK");

        return new List<Book>
            {
                new (
                    Guid.NewGuid(),
                    "ISBN001",
                    "C# in Depth",
                    "Non-Fiction",
                    "Deep dive into C#.",
                    author1.Id,
                    author1,
                    "url1"),

                new (
                    Guid.NewGuid(),
                    "ISBN002",
                    "The Great Gatsby",
                    "Fiction",
                    "Classic novel.",
                    author2.Id,
                    author2,
                    "url2"),

                new (
                    Guid.NewGuid(),
                    "ISBN003",
                    "Entity Framework Core",
                    "Non-Fiction",
                    "Guide to EF Core.",
                    author1.Id,
                    author1,
                    "url3"),

                new (
                    Guid.NewGuid(),
                    "ISBN004",
                    "Cooking 101",
                    "Non-Fiction",
                    "Basics of cooking.",
                    author2.Id,
                    author2,
                    "url4")
            };
    }

    private Mock<IBookRepository> SetupRepositoryMock(List<Book> books)
    {
        var repositoryMock = new Mock<IBookRepository>();

        repositoryMock.Setup(r => r.Query())
            .Returns(books.AsQueryable());

        repositoryMock.Setup(r => r.GetPagedAsync(
                It.IsAny<IQueryable<BookResponse>>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
            .Returns((IQueryable<BookResponse> query, int page, int pageSize) =>
            {
                int totalCount = query.Count();
                var items = query.Skip((page - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToList();
                return Task.FromResult(new PagedList<BookResponse>(items, page, pageSize, totalCount));
            });

        return repositoryMock;
    }

    [Fact]
    public async Task Handle_Should_ShowOnlyAvailable_WhenShowUnavailableFalse()
    {
        //Arrange
        var showUnavailable = false;

        var query = new GetBooksQuery(null, [], [], showUnavailable, null, null, 1, 10);

        //Act
        var result = await _handler.Handle(query, CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.Items.Should().OnlyContain(bookResponse => bookResponse.IsAvailable == true);
    }

    [Fact]
    public async Task Handle_Should_ReturnBooksByAuthorFilter_WhenAuthorFilterApplied()
    {
        //Arrange
        var targetAuthorId = _books.First().AuthorId;

        var query = new GetBooksQuery(null, [], [targetAuthorId], true, null, null, 1, 10);

        //Act
        var result = await _handler.Handle(query, CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.Items.Should().OnlyContain(bookResponse =>
            bookResponse.Author.Id == targetAuthorId);
    }

    [Fact]
    public async Task Handle_Should_ReturnBooksByGenreFilter_WhenGenreFilterApplied()
    {
        //Arrange
        string targetGenre = "Fiction";

        var query = new GetBooksQuery(null, [targetGenre], [], true, null, null, 1, 10);

        //Act
        var result = await _handler.Handle(query, CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.Items.Should().OnlyContain(bookResponse =>
            bookResponse.Genree == targetGenre);
    }

    [Fact]
    public async Task Handle_Should_ReturnBooksMathingSearchTerm_WhenSearchTermNotNullOrWhiteSpaces()
    {
        //Arrange
        string searhTerm = "entity";

        var query = new GetBooksQuery(searhTerm, [], [], true, null, null, 1, 10);

        //Act
        var result = await _handler.Handle(query, CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.Items.Should().HaveCount(1);
        result.Items.First().Title.Should().ContainEquivalentOf("Entity Framework Core");
    }

    [Fact]
    public async Task Handle_Should_SortBooksDescending_WhenSortOrderDsc()
    {
        //Arrange
        string sortOrder = "desc";

        var query = new GetBooksQuery(null, [], [], true, null, sortOrder, 1, 10);

        //Act
        var result = await _handler.Handle(query, CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        var expectedOrder = result.Items.OrderByDescending(b => b.Title).Select(b => b.Id);
        result.Items.Select(b => b.Id).Should().Equal(expectedOrder);
    }

    [Fact]
    public async Task Handle_Should_SortBooksByGenre_WhenSortColumnGenre()
    {
        //Arrange
        string sortColumn = "genree";

        var query = new GetBooksQuery(null, [], [], true, sortColumn, null, 1, 10);

        //Act
        var result = await _handler.Handle(query, CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        var expectedOrder = result.Items.OrderBy(b => b.Genree).Select(b => b.Id);
        result.Items.Select(b => b.Id).Should().Equal(expectedOrder);
    }
}
