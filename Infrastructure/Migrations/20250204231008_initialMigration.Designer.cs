﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250204231008_initialMigration")]
    partial class initialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Core.Entities.Author", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Authors", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("62e21ba9-9377-44e7-9043-85b4eec3b0a5"),
                            Country = "United Kingdom",
                            DateOfBirth = new DateTime(1965, 7, 31, 0, 0, 0, 0, DateTimeKind.Utc),
                            FirstName = "J.K.",
                            LastName = "Rowling"
                        },
                        new
                        {
                            Id = new Guid("a3e96cc7-f74a-4c9b-857a-7a8a4ff15d59"),
                            Country = "United Kingdom",
                            DateOfBirth = new DateTime(1903, 6, 25, 0, 0, 0, 0, DateTimeKind.Utc),
                            FirstName = "George",
                            LastName = "Orwell"
                        },
                        new
                        {
                            Id = new Guid("3a8f8be2-5c8e-4f9d-a20a-e788b01b1bd8"),
                            Country = "United States",
                            DateOfBirth = new DateTime(1896, 9, 24, 0, 0, 0, 0, DateTimeKind.Utc),
                            FirstName = "F. Scott",
                            LastName = "Fitzgerald"
                        },
                        new
                        {
                            Id = new Guid("3f965ba7-3962-4c5e-8b0b-67325256d865"),
                            Country = "United States",
                            DateOfBirth = new DateTime(1926, 4, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                            FirstName = "Harper",
                            LastName = "Lee"
                        },
                        new
                        {
                            Id = new Guid("8c98ef9a-b29b-46b1-bcca-e31b39c46568"),
                            Country = "United Kingdom",
                            DateOfBirth = new DateTime(1775, 12, 16, 0, 0, 0, 0, DateTimeKind.Utc),
                            FirstName = "Jane",
                            LastName = "Austen"
                        },
                        new
                        {
                            Id = new Guid("37c2b95e-bf1b-41b9-bcca-10aef794e913"),
                            Country = "United States",
                            DateOfBirth = new DateTime(1835, 11, 30, 0, 0, 0, 0, DateTimeKind.Utc),
                            FirstName = "Mark",
                            LastName = "Twain"
                        },
                        new
                        {
                            Id = new Guid("5a95136f-a3f0-4bdf-8ab0-3996999a789a"),
                            Country = "Russia",
                            DateOfBirth = new DateTime(1828, 9, 9, 0, 0, 0, 0, DateTimeKind.Utc),
                            FirstName = "Leo",
                            LastName = "Tolstoy"
                        },
                        new
                        {
                            Id = new Guid("6473ccb9-f849-4946-b044-8d261a10cf0a"),
                            Country = "Colombia",
                            DateOfBirth = new DateTime(1927, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc),
                            FirstName = "Gabriel",
                            LastName = "Garcia Marquez"
                        },
                        new
                        {
                            Id = new Guid("e91bde0b-cd01-4c52-8245-f6278fe7b9a4"),
                            Country = "United States",
                            DateOfBirth = new DateTime(1819, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            FirstName = "Herman",
                            LastName = "Melville"
                        },
                        new
                        {
                            Id = new Guid("49d08119-1428-4b47-b1b6-ec9077cfb877"),
                            Country = "United Kingdom",
                            DateOfBirth = new DateTime(1797, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc),
                            FirstName = "Mary",
                            LastName = "Shelley"
                        },
                        new
                        {
                            Id = new Guid("cea9d38a-1119-47b1-8ae8-c6b3a72d919e"),
                            Country = "United Kingdom",
                            DateOfBirth = new DateTime(1892, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc),
                            FirstName = "J.R.R.",
                            LastName = "Tolkien"
                        },
                        new
                        {
                            Id = new Guid("63ccc0b6-a595-4d38-92a8-2db150073698"),
                            Country = "United States",
                            DateOfBirth = new DateTime(1899, 7, 21, 0, 0, 0, 0, DateTimeKind.Utc),
                            FirstName = "Ernest",
                            LastName = "Hemingway"
                        });
                });

            modelBuilder.Entity("Core.Entities.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Genree")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("boolean");

                    b.Property<string>("Isbn")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Books", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("423ec25c-2c07-4c36-a95f-d38669dcc590"),
                            AuthorId = new Guid("62e21ba9-9377-44e7-9043-85b4eec3b0a5"),
                            Description = "The book that started it all.",
                            Genree = "Fantasy",
                            ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/51UoqRAxwEL._SX331_BO1,204,203,200_.jpg",
                            IsAvailable = true,
                            Isbn = "9781408855652",
                            Title = "Harry Potter and the Philosopher's Stone"
                        },
                        new
                        {
                            Id = new Guid("ea188a26-0fb4-4a81-a969-bf0e431eee29"),
                            AuthorId = new Guid("a3e96cc7-f74a-4c9b-857a-7a8a4ff15d59"),
                            Description = "A novel about a dystopian future.",
                            Genree = "Fiction",
                            ImageUrl = "https://m.media-amazon.com/images/I/71rpa1-kyvL._AC_UF894,1000_QL80_.jpg",
                            IsAvailable = true,
                            Isbn = "9780451524935",
                            Title = "1984"
                        },
                        new
                        {
                            Id = new Guid("1cc33180-bac0-4a7d-8894-c3596ea3c004"),
                            AuthorId = new Guid("3a8f8be2-5c8e-4f9d-a20a-e788b01b1bd8"),
                            Description = "A story of the Jazz Age in the United States.",
                            Genree = "Romance",
                            ImageUrl = "https://m.media-amazon.com/images/I/81TLiZrasVL._AC_UF1000,1000_QL80_.jpg",
                            IsAvailable = true,
                            Isbn = "9780743273565",
                            Title = "The Great Gatsby"
                        },
                        new
                        {
                            Id = new Guid("a2e79b1c-a9fa-46ec-955e-74c893dc49e1"),
                            AuthorId = new Guid("3f965ba7-3962-4c5e-8b0b-67325256d865"),
                            Description = "A novel about racial injustice in the Deep South.",
                            Genree = "Fiction",
                            ImageUrl = "https://m.media-amazon.com/images/I/81JYG8sqRsL._AC_UF1000,1000_QL80_.jpg",
                            IsAvailable = true,
                            Isbn = "9780061120084",
                            Title = "To Kill a Mockingbird"
                        },
                        new
                        {
                            Id = new Guid("bd9c80b5-138b-4ef5-a091-f9ca697cc260"),
                            AuthorId = new Guid("8c98ef9a-b29b-46b1-bcca-e31b39c46568"),
                            Description = "A classic novel about manners and marriage.",
                            Genree = "Romance",
                            ImageUrl = "https://images-eu.ssl-images-amazon.com/images/I/81NLDvyAHrL._AC_UL600_SR600,600_.jpg",
                            IsAvailable = true,
                            Isbn = "9781503290563",
                            Title = "Pride and Prejudice"
                        },
                        new
                        {
                            Id = new Guid("e7275cc0-c33a-4023-a8ee-be028691bc76"),
                            AuthorId = new Guid("37c2b95e-bf1b-41b9-bcca-10aef794e913"),
                            Description = "A novel about the adventures of a young boy along the Mississippi River.",
                            Genree = "Fiction",
                            ImageUrl = "https://images-eu.ssl-images-amazon.com/images/I/91yLxd90MiL._AC_UL600_SR600,600_.jpg",
                            IsAvailable = true,
                            Isbn = "9780486280615",
                            Title = "Adventures of Huckleberry Finn"
                        },
                        new
                        {
                            Id = new Guid("b4d899ce-ae21-459c-8ca9-cf25f91e07b7"),
                            AuthorId = new Guid("5a95136f-a3f0-4bdf-8ab0-3996999a789a"),
                            Description = "A historical novel set during the Napoleonic Wars.",
                            Genree = "Fiction",
                            ImageUrl = "https://blackwells.co.uk/jacket/l/9780199232765.webp",
                            IsAvailable = true,
                            Isbn = "9780199232765",
                            Title = "War and Peace"
                        },
                        new
                        {
                            Id = new Guid("65a4b373-6096-4b7f-bf51-28196f42d34c"),
                            AuthorId = new Guid("6473ccb9-f849-4946-b044-8d261a10cf0a"),
                            Description = "A landmark novel in Latin American literature.",
                            Genree = "Fiction",
                            ImageUrl = "https://m.media-amazon.com/images/I/71WQQjgEhQL.jpg",
                            IsAvailable = true,
                            Isbn = "9781400034934",
                            Title = "One Hundred Years of Solitude"
                        },
                        new
                        {
                            Id = new Guid("0c93d826-eed6-4e8d-ad55-11bd4a8d0f33"),
                            AuthorId = new Guid("e91bde0b-cd01-4c52-8245-f6278fe7b9a4"),
                            Description = "A novel about Captain Ahab's obsession with a great white whale.",
                            Genree = "Fiction",
                            ImageUrl = "https://images.booksense.com/images/786/280/9781503280786.jpg",
                            IsAvailable = true,
                            Isbn = "9781503280786",
                            Title = "Moby-Dick"
                        },
                        new
                        {
                            Id = new Guid("525bdebf-83c8-49c4-b5cd-013c327cc3e2"),
                            AuthorId = new Guid("49d08119-1428-4b47-b1b6-ec9077cfb877"),
                            Description = "A Gothic novel about a scientist who creates life.",
                            Genree = "Science Fiction",
                            ImageUrl = "https://pictures.abebooks.com/isbn/9780486282114-uk.jpg",
                            IsAvailable = true,
                            Isbn = "9780486282114",
                            Title = "Frankenstein"
                        },
                        new
                        {
                            Id = new Guid("3593d89b-6c57-4f94-a8e5-09129d913701"),
                            AuthorId = new Guid("cea9d38a-1119-47b1-8ae8-c6b3a72d919e"),
                            Description = "An epic high-fantasy novel.",
                            Genree = "Fantasy",
                            ImageUrl = "https://pictures.abebooks.com/isbn/9780618640157-uk.jpg",
                            IsAvailable = true,
                            Isbn = "9780618640157",
                            Title = "The Lord of the Rings"
                        },
                        new
                        {
                            Id = new Guid("97d6a6d4-9096-4380-8e10-a35a8568fcc9"),
                            AuthorId = new Guid("63ccc0b6-a595-4d38-92a8-2db150073698"),
                            Description = "A novel about an aging fisherman's battle with a giant marlin.",
                            Genree = "Fiction",
                            ImageUrl = "https://m.media-amazon.com/images/I/71+rqbp57dL._AC_UF894,1000_QL80_.jpg",
                            IsAvailable = true,
                            Isbn = "9780684801469",
                            Title = "The Old Man and the Sea"
                        });
                });

            modelBuilder.Entity("Core.Entities.BookLoan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BookId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("LoanDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("BookLoans", (string)null);
                });

            modelBuilder.Entity("Core.Entities.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ExpiresOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Token")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Core.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Core.Entities.Book", b =>
                {
                    b.HasOne("Core.Entities.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Core.Entities.BookLoan", b =>
                {
                    b.HasOne("Core.Entities.Book", "Book")
                        .WithMany("BookLoans")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.User", "User")
                        .WithMany("BookLoans")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Entities.RefreshToken", b =>
                {
                    b.HasOne("Core.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Entities.Author", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("Core.Entities.Book", b =>
                {
                    b.Navigation("BookLoans");
                });

            modelBuilder.Entity("Core.Entities.User", b =>
                {
                    b.Navigation("BookLoans");
                });
#pragma warning restore 612, 618
        }
    }
}
