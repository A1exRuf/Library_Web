using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configurations;

internal sealed class BookrConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books");

        builder.HasKey(book => book.Id);

        builder.Property(book => book.Isbn);

        builder.Property(book => book.Title);

        builder.Property(book => book.Genree);

        builder.Property(book => book.Description);

        builder.Property(book => book.AuthorId);

        builder.Property(book => book.TakenAt);
    }
}
