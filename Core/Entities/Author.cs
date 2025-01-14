using Core.Primitives;

namespace Core.Entities;

public sealed class Author : Entity
{
    public string FirstName { get; set; }

    public string SecondName { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string Country { get; set; }

    public Author(Guid id, string firstName, string secondName,
        DateTime dateOfBirth, string country) : base(id)
    {
        FirstName = firstName;
        SecondName = secondName;
        DateOfBirth = dateOfBirth;
        Country = country;
    }

    public Author() { }
}
