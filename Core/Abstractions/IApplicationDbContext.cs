using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace Core.Abstractions;

public interface IApplicationDbContext
{
    DbSet<Author> Authors { get; }
    DbSet<Book> Books { get; }
    DbSet<User> Users { get; }
    DbSet<BookLoan> BookLoans { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
