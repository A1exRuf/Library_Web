using Core.Primitives;

namespace Core.Entities;

public sealed class BookLoan : Entity
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid BookId { get; set; }
    public Book Book { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }

    public BookLoan(Guid id, User user, Guid userId, Book book, Guid bookId, DateTime loanDate) : base(id)
    {
        User = user;
        UserId = userId;
        Book = book;
        BookId = bookId;
        LoanDate = loanDate;
        DueDate = loanDate.AddMonths(3);
    }

    public BookLoan() { }
}
