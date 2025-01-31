using Microsoft.EntityFrameworkCore;
using Core.Abstractions;
using Core.Entities;

namespace Infrastructure;

public sealed class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<Author> Authors { get; set; }

    public DbSet<Book> Books { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public DbSet<BookLoan> BookLoans { get; set; }

    public ApplicationDbContext(DbContextOptions options)
        : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
}
