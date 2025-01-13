using Core.Primitives;


namespace Core.Entities;

public sealed class Author : Entity
{
    public string FirstName { get; private set; }

    public string SecondName { get; private set; }

    public DateTime DateOfBirth { get; private set; }

    public string Country { get; private set; }

    public Author(Guid id, string firstName, string secondName,
        DateTime dateOfBirth, string country) : base(id)
    {
        FirstName = firstName;
        SecondName = secondName;
        DateOfBirth = dateOfBirth;
        Country = country;
        
    }
}
