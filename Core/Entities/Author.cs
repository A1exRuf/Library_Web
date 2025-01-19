using Core.Primitives;

namespace Core.Entities;

public sealed class Author : Entity
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string Country { get; set; }

    public ICollection<Book> Books { get; set; } = new List<Book>();

    public Author(Guid id, string firstName, string lastName,
        DateTime dateOfBirth, string country) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Country = country;
    }

    public Author() { }
}
