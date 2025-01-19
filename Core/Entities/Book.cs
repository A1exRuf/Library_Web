using Core.Primitives;
using System.Collections;

namespace Core.Entities;

public sealed class Book : Entity
{
    public string Isbn { get; set; }
    public string Title { get; set; }
    public string Genree { get; set; }
    public string Description { get; set; }
    public Guid AuthorId { get; set; }
    public Author Author { get; set; }
    public Guid? ImageId { get; set; }
    public bool IsAvailable { get; set; }
    public ICollection<BookLoan> BookLoans { get; set; } = new List<BookLoan>();

    public Book( Guid id, string isbn, string title, 
        string genree, string description, Guid authorId, Author author) : base(id)
    {
        Isbn = isbn;
        Title = title;
        Genree = genree;
        Description = description;
        AuthorId = authorId;
        Author = author;
        IsAvailable = true;
    }

    public Book()
    {
    }
}
