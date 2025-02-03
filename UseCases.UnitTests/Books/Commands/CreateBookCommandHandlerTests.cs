using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using FluentAssertions;
using Moq;
using System.Net;
using UseCases.Books.Commands.CreateBook;

namespace UseCases.UnitTests.Books.Commands;

public class CreateBookCommandHandlerTests
{
    private readonly Mock<IAuthorRepository> _authorRepositoryMock;
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly Mock<IBlobService> _blobServiceMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CreateBookCommandHandler _handler;

    public CreateBookCommandHandlerTests()
    {
        _authorRepositoryMock = new Mock<IAuthorRepository>();
        _bookRepositoryMock = new Mock<IBookRepository>();
        _blobServiceMock = new Mock<IBlobService>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _handler = new CreateBookCommandHandler(
            _authorRepositoryMock.Object,
            _bookRepositoryMock.Object,
            _blobServiceMock.Object,
            _unitOfWorkMock.Object);
    }

    private Author author = new Author(
            Guid.NewGuid(),
            "FirstName",
            "SecondName",
            DateTime.UtcNow,
            "Country");

    [Fact]
    public async Task Handle_ShouldCreateBook_WhenAuthorExists()
    {
        // Arrange
        var command = new CreateBookCommand("00000000000", "Title", "Fiction", "Description", author.Id, null);

        _authorRepositoryMock.Setup(repo => repo.GetByIdAsync(author.Id))
            .ReturnsAsync(author);

        _bookRepositoryMock.Setup(repo => repo.Add(It.IsAny<Book>()));

        _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();
        _bookRepositoryMock.Verify(repo => repo.Add(It.IsAny<Book>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenAuthorNotFound()
    {
        // Arrange
        var authorId = Guid.NewGuid();
        var command = new CreateBookCommand("1234567890", "Test Book", "Fiction", "Description", authorId, null);

        _authorRepositoryMock.Setup(repo => repo.GetByIdAsync(authorId))
            .ReturnsAsync((Author?)null);

        // Act & Assert
        await Assert.ThrowsAsync<AuthorNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        _bookRepositoryMock.Verify(repo => repo.Add(It.IsAny<Book>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldUploadImage_WhenImageProvided()
    {
        // Arrange
        var imageStream = new MemoryStream();
        var command = new CreateBookCommand("00000000000", "Title", "Fiction", "Description", author.Id, imageStream);

        _authorRepositoryMock.Setup(repo => repo.GetByIdAsync(author.Id))
            .ReturnsAsync(author);

        var bookId = Guid.NewGuid(); 
        string expectedImageUrl = $"http://127.0.0.1:10000/devstoreaccount1/bimages/{bookId}_img";

        _blobServiceMock.Setup(blob => blob.UploadAsync(It.IsAny<Stream>(), It.IsAny<string>(), "bimages"))
            .ReturnsAsync(expectedImageUrl);

        _bookRepositoryMock.Setup(repo => repo.Add(It.Is<Book>(b => b.ImageUrl == expectedImageUrl && b.Id == bookId)));

        _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();
        _blobServiceMock.Verify(blob => blob.UploadAsync(It.IsAny<Stream>(), It.IsAny<string>(), "bimages"), Times.Once);
        _bookRepositoryMock.Verify(repo => repo.Add(It.Is<Book>(b => b.ImageUrl == expectedImageUrl && b.Id != Guid.Empty)), Times.Once);
    }
}
