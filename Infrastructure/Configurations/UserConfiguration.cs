using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(User => User.Id);

        builder.Property(User => User.Name);

        builder.Property(User => User.Email);

        builder.Property(User => User.PasswordHash);

        builder.Property(User => User.Role);
    }
}
