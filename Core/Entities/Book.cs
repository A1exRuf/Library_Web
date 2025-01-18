using Core.Primitives;

namespace Core.Entities;

public sealed class Book : Entity
{
    public string Isbn { get; set; }

    public string Title { get; set; }

    public string Genree { get; set; }

    public string Description { get; set; }
    
    public Guid AuthorId { get; set; }

    public Author Author { get; set; }

    public DateTime? TakenAt { get; set; }

    public Guid? ImageId { get; set; }

    public Book( Guid id, string isbn, string title, 
        string genree, string description, Guid authorId, Author author, Guid? imageId = null) : base(id)
    {
        Isbn = isbn;
        Title = title;
        Genree = genree;
        Description = description;
        AuthorId = authorId;
        Author = author;
    }

    public Book()
    {
    }

    public void SetTakenAt(DateTime takenAt) => TakenAt = takenAt;

    public void ClearTakenAt() => TakenAt = null;
}
