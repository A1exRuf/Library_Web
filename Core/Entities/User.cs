using Core.Primitives;


namespace Core.Entities;

public class User : Entity
{
    public string Name { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public string Role { get; set; }

    public ICollection<BookLoan> BookLoans { get; set; } = new List<BookLoan>();

    public User(Guid id, string name, string email, string passwordHash, string role) : base(id)
    {
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
    }

    public User() { }
}