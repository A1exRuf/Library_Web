using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using FluentAssertions;
using Moq;
using UseCases.Books.Commands.DeleteBook;

namespace UseCases.UnitTests.Books.Commands;

public class DeleteBookCommandHandlerTests
{
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly Mock<IBlobService> _blobServiceMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly DeleteBookCommandHandler _handler;

    public DeleteBookCommandHandlerTests()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();
        _blobServiceMock = new Mock<IBlobService>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _handler = new DeleteBookCommandHandler(
            _bookRepositoryMock.Object,
            _blobServiceMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_Should_DeleteBook_WhenBookExists()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        var authorId = Guid.NewGuid();

        var author = new Author(
            authorId,
            "FirstName",
            "SecondName",
            DateTime.UtcNow,
            "Country");

        var book = new Book(bookId, "00000000000", "Title", "Fiction", "Description", authorId, author, null);

        _bookRepositoryMock.Setup(repo => repo.GetByIdAsync(bookId))
            .ReturnsAsync(book);

        // Act
        var result = await _handler.Handle(new DeleteBookCommand(bookId), CancellationToken.None);

        // Assert
        result.Should().BeTrue();
        _bookRepositoryMock.Verify(repo => repo.Remove(book), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_ThrowException_WhenNotExist()
    {
        // Arrange
        var bookId = Guid.NewGuid();

        _bookRepositoryMock.Setup(repo => repo.GetByIdAsync(bookId))
            .ReturnsAsync((Book?)null);

        // Act & Assert
        await Assert.ThrowsAsync<BookNotFoundException>(() =>
            _handler.Handle(new DeleteBookCommand(bookId), CancellationToken.None));

        _bookRepositoryMock.Verify(repo => repo.Remove(It.IsAny<Book>()), Times.Never);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_Should_DeleteBookImage_WhenImageExists()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        var authorId = Guid.NewGuid();

        var author = new Author(
            authorId,
            "FirstName",
            "SecondName",
            DateTime.UtcNow,
            "Country");

        var book = new Book(bookId, "00000000000", "Title", "Fiction", "Description", authorId, author, "image_url");

        _bookRepositoryMock.Setup(repo => repo.GetByIdAsync(bookId))
            .ReturnsAsync(book);

        // Act
        var result = await _handler.Handle(new DeleteBookCommand(bookId), CancellationToken.None);

        // Assert
        result.Should().BeTrue();
        _blobServiceMock.Verify(blob => blob.DeleteAsync("image_url"), Times.Once);
        _bookRepositoryMock.Verify(repo => repo.Remove(book), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}