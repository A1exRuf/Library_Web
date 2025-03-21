﻿using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configurations;

internal sealed class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books");

        builder.HasKey(book => book.Id);

        builder.Property(book => book.Isbn).IsRequired();

        builder.Property(book => book.Title).IsRequired();

        builder.Property(book => book.Genree).IsRequired();

        builder.Property(book => book.Description);

        builder.HasOne(book => book.Author)
            .WithMany(author => author.Books)
            .HasForeignKey(book => book.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(book => book.IsAvailable).IsRequired();

        builder.Property(book => book.ImageUrl);
    }
}
