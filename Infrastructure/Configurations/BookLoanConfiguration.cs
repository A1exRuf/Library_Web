using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal sealed class BookLoanConfiguration : IEntityTypeConfiguration<BookLoan>
{
    public void Configure(EntityTypeBuilder<BookLoan> builder)
    {
        builder.ToTable("BookLoans");

        builder.HasKey(BookLoan => BookLoan.Id);

        builder.Property(BookLoan => BookLoan.UserId);

        builder.HasOne(BookLoan => BookLoan.User)
            .WithMany(User => User.BookLoans)
            .HasForeignKey(BookLoan => BookLoan.UserId);

        builder.Property(BookLoan => BookLoan.BookId);

        builder.HasOne(BookLoan => BookLoan.Book)
            .WithMany(book => book.BookLoans)
            .HasForeignKey(book => book.BookId);

        builder.Property(BookLoan => BookLoan.LoanDate);

        builder.Property(BookLoan => BookLoan.DueDate);
    }
}
