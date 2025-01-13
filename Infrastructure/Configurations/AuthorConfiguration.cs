using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal sealed class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.ToTable("Authors");

        builder.HasKey(author => author.Id);

        builder.Property(author => author.FirstName).HasMaxLength(50);

        builder.Property(author => author.SecondName).HasMaxLength(50);

        builder.Property(author => author.DateOfBirth).IsRequired();

        builder.Property(author => author.Country).HasMaxLength(50);
    }
}
