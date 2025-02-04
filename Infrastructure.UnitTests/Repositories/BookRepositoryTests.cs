using Core.Entities;
using FluentAssertions;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.UnitTests.Repositories;

public class BookRepositoryTests
{
    private readonly ApplicationDbContext _context;
    private readonly BookRepository _repository;
    private readonly UnitOfWork _unitOfWork;
    private readonly Book _book;
    public BookRepositoryTests()
    {
        DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder()
            .UseInMemoryDatabase(Guid.NewGuid().ToString());

        _context = new ApplicationDbContext(dbOptions.Options);
        _repository = new BookRepository(_context);
        _unitOfWork = new UnitOfWork(_context);
        _book = GetTestBook();

    }

    private Book GetTestBook()
    {
        var authorId = Guid.NewGuid();

        var book = new Book(
            Guid.NewGuid(),
            "00000000000",
            "Title",
            "Fiction",
            "Description",
            authorId,
            new(
                authorId,
                "FirstName",
                "LastName",
                new DateTime(),
                "Country"
                ),
            "Url"
            );

        return book;
    }

    [Fact]
    public async Task Add_Should_AddBookToDatabase()
    {
        //Arrage & Act
        _repository.Add(_book);

        await _unitOfWork.SaveChangesAsync();

        //Assert
        var insertedBook = await _context.Books.Include(b => b.Author)
                                                   .SingleOrDefaultAsync(b => b.Id == _book.Id);
        insertedBook.Should().NotBeNull();
        insertedBook!.Title.Should().Be(_book.Title);
        insertedBook.Author.Should().NotBeNull();
        insertedBook.Author.FirstName.Should().Be(_book.Author.FirstName);
    }

    [Fact]
    public async Task GetBookById_Should_ReturnBook_WhenBookExist()
    {
        //Arrage
        _context.Books.Add(_book);
        await _context.SaveChangesAsync();

        //Act
        var retrievedBook = await _repository.GetByIdAsync( _book.Id );

        //Assert
        retrievedBook.Should().NotBeNull();
        retrievedBook!.Id.Should().Be(_book.Id);
        retrievedBook.Author.Should().NotBeNull();
        retrievedBook.Author.Id.Should().Be(_book.Author.Id);
    }
    [Fact]
    public async Task GetBookById_Should_ThrowException_WhenBookNotExist()
    {
        //Arrage & Act
        var retrievedBook = await _repository.GetByIdAsync(_book.Id);

        //Assert
        retrievedBook.Should().BeNull();
    }
}
