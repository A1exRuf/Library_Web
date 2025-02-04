using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using FluentAssertions;
using Moq;
using UseCases.Books.Commands.UpdateBook;

namespace UseCases.UnitTests.Books.Commands;

public class UpdateBookCommandHandlerTests
{
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly Mock<IBlobService> _blobServiceMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly UpdateBookCommandHandler _handler;

    public UpdateBookCommandHandlerTests()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();
        _blobServiceMock = new Mock<IBlobService>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _handler = new UpdateBookCommandHandler(
            _bookRepositoryMock.Object,
            _blobServiceMock.Object,
            _unitOfWorkMock.Object);
    }

    private readonly Author _author = new(
            Guid.NewGuid(),
            "FirstName",
            "SecondName",
            DateTime.UtcNow,
            "Country");

    private readonly Book _book = new()
    {
        Isbn = "00000000000",
        Title = "Old Title",
        Genree = "Fiction",
        Description = "Old Description",
        AuthorId = Guid.NewGuid()
    };

    [Fact]
    public async Task Handle_Should_UpdateBook_WhenBookExists()
    {
        // Arrange
        var command = new UpdateBookCommand(_book.Id, "0000000000", "Updated Title", "Updated Genre", "Updated Description", _author.Id, null);

        _bookRepositoryMock.Setup(repo => repo.GetByIdAsync(_book.Id))
            .ReturnsAsync(_book);

        _bookRepositoryMock.Setup(repo => repo.Update(It.IsAny<Book>()));

        _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
        _book.Isbn.Should().Be("0000000000");
        _book.Title.Should().Be("Updated Title");
        _book.Genree.Should().Be("Updated Genre");
        _book.Description.Should().Be("Updated Description");
        _book.AuthorId.Should().Be(_author.Id);

        _bookRepositoryMock.Verify(repo => repo.Update(It.IsAny<Book>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_ThrowException_WhenNotExist()
    {
        // Arrange
        var nonExistentBookId = Guid.NewGuid();
        var command = new UpdateBookCommand(nonExistentBookId, "0000000000", "Updated Title", "Updated Genre", "Updated Description", _author.Id, null);

        _bookRepositoryMock.Setup(repo => repo.GetByIdAsync(nonExistentBookId))
            .ReturnsAsync((Book?)null);

        // Act & Assert
        await Assert.ThrowsAsync<BookNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        _bookRepositoryMock.Verify(repo => repo.Update(It.IsAny<Book>()), Times.Never);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_Should_UploadImage_WhenImageProvided()
    {
        // Arrange
        var imageStream = new MemoryStream();
        var command = new UpdateBookCommand(
            _book.Id, 
            "0000000000", 
            "Updated Title", 
            "Updated Genre", 
            "Updated Description", 
            _author.Id,
        imageStream);

        string expectedImageUrl = $"http://127.0.0.1:10000/devstoreaccount1/bimages/{_book.Id}_img";

        _bookRepositoryMock.Setup(repo => repo.GetByIdAsync(_book.Id))
            .ReturnsAsync(_book);

        _blobServiceMock.Setup(blob => blob.UploadAsync(It.IsAny<Stream>(), It.IsAny<string>(), "bimages"))
            .ReturnsAsync(expectedImageUrl);

        _bookRepositoryMock.Setup(repo => repo.Update(It.IsAny<Book>()));

        _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
        _blobServiceMock.Verify(blob => blob.UploadAsync(It.IsAny<Stream>(), It.IsAny<string>(), "bimages"), Times.Once);
        _bookRepositoryMock.Verify(repo => repo.Update(It.Is<Book>(b => b.ImageUrl == expectedImageUrl)), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
