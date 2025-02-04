using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Country = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Isbn = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Genree = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    IsAvailable = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpiresOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookLoans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    BookId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoanDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookLoans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookLoans_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookLoans_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Country", "DateOfBirth", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("37c2b95e-bf1b-41b9-bcca-10aef794e913"), "United States", new DateTime(1835, 11, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Mark", "Twain" },
                    { new Guid("3a8f8be2-5c8e-4f9d-a20a-e788b01b1bd8"), "United States", new DateTime(1896, 9, 24, 0, 0, 0, 0, DateTimeKind.Utc), "F. Scott", "Fitzgerald" },
                    { new Guid("3f965ba7-3962-4c5e-8b0b-67325256d865"), "United States", new DateTime(1926, 4, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Harper", "Lee" },
                    { new Guid("49d08119-1428-4b47-b1b6-ec9077cfb877"), "United Kingdom", new DateTime(1797, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Mary", "Shelley" },
                    { new Guid("5a95136f-a3f0-4bdf-8ab0-3996999a789a"), "Russia", new DateTime(1828, 9, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Leo", "Tolstoy" },
                    { new Guid("62e21ba9-9377-44e7-9043-85b4eec3b0a5"), "United Kingdom", new DateTime(1965, 7, 31, 0, 0, 0, 0, DateTimeKind.Utc), "J.K.", "Rowling" },
                    { new Guid("63ccc0b6-a595-4d38-92a8-2db150073698"), "United States", new DateTime(1899, 7, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Ernest", "Hemingway" },
                    { new Guid("6473ccb9-f849-4946-b044-8d261a10cf0a"), "Colombia", new DateTime(1927, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Gabriel", "Garcia Marquez" },
                    { new Guid("8c98ef9a-b29b-46b1-bcca-e31b39c46568"), "United Kingdom", new DateTime(1775, 12, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Jane", "Austen" },
                    { new Guid("a3e96cc7-f74a-4c9b-857a-7a8a4ff15d59"), "United Kingdom", new DateTime(1903, 6, 25, 0, 0, 0, 0, DateTimeKind.Utc), "George", "Orwell" },
                    { new Guid("cea9d38a-1119-47b1-8ae8-c6b3a72d919e"), "United Kingdom", new DateTime(1892, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), "J.R.R.", "Tolkien" },
                    { new Guid("e91bde0b-cd01-4c52-8245-f6278fe7b9a4"), "United States", new DateTime(1819, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Herman", "Melville" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Description", "Genree", "ImageUrl", "IsAvailable", "Isbn", "Title" },
                values: new object[,]
                {
                    { new Guid("0c93d826-eed6-4e8d-ad55-11bd4a8d0f33"), new Guid("e91bde0b-cd01-4c52-8245-f6278fe7b9a4"), "A novel about Captain Ahab's obsession with a great white whale.", "Fiction", "https://images.booksense.com/images/786/280/9781503280786.jpg", true, "9781503280786", "Moby-Dick" },
                    { new Guid("1cc33180-bac0-4a7d-8894-c3596ea3c004"), new Guid("3a8f8be2-5c8e-4f9d-a20a-e788b01b1bd8"), "A story of the Jazz Age in the United States.", "Romance", "https://m.media-amazon.com/images/I/81TLiZrasVL._AC_UF1000,1000_QL80_.jpg", true, "9780743273565", "The Great Gatsby" },
                    { new Guid("3593d89b-6c57-4f94-a8e5-09129d913701"), new Guid("cea9d38a-1119-47b1-8ae8-c6b3a72d919e"), "An epic high-fantasy novel.", "Fantasy", "https://pictures.abebooks.com/isbn/9780618640157-uk.jpg", true, "9780618640157", "The Lord of the Rings" },
                    { new Guid("423ec25c-2c07-4c36-a95f-d38669dcc590"), new Guid("62e21ba9-9377-44e7-9043-85b4eec3b0a5"), "The book that started it all.", "Fantasy", "https://images-na.ssl-images-amazon.com/images/I/51UoqRAxwEL._SX331_BO1,204,203,200_.jpg", true, "9781408855652", "Harry Potter and the Philosopher's Stone" },
                    { new Guid("525bdebf-83c8-49c4-b5cd-013c327cc3e2"), new Guid("49d08119-1428-4b47-b1b6-ec9077cfb877"), "A Gothic novel about a scientist who creates life.", "Science Fiction", "https://pictures.abebooks.com/isbn/9780486282114-uk.jpg", true, "9780486282114", "Frankenstein" },
                    { new Guid("65a4b373-6096-4b7f-bf51-28196f42d34c"), new Guid("6473ccb9-f849-4946-b044-8d261a10cf0a"), "A landmark novel in Latin American literature.", "Fiction", "https://m.media-amazon.com/images/I/71WQQjgEhQL.jpg", true, "9781400034934", "One Hundred Years of Solitude" },
                    { new Guid("97d6a6d4-9096-4380-8e10-a35a8568fcc9"), new Guid("63ccc0b6-a595-4d38-92a8-2db150073698"), "A novel about an aging fisherman's battle with a giant marlin.", "Fiction", "https://m.media-amazon.com/images/I/71+rqbp57dL._AC_UF894,1000_QL80_.jpg", true, "9780684801469", "The Old Man and the Sea" },
                    { new Guid("a2e79b1c-a9fa-46ec-955e-74c893dc49e1"), new Guid("3f965ba7-3962-4c5e-8b0b-67325256d865"), "A novel about racial injustice in the Deep South.", "Fiction", "https://m.media-amazon.com/images/I/81JYG8sqRsL._AC_UF1000,1000_QL80_.jpg", true, "9780061120084", "To Kill a Mockingbird" },
                    { new Guid("b4d899ce-ae21-459c-8ca9-cf25f91e07b7"), new Guid("5a95136f-a3f0-4bdf-8ab0-3996999a789a"), "A historical novel set during the Napoleonic Wars.", "Fiction", "https://blackwells.co.uk/jacket/l/9780199232765.webp", true, "9780199232765", "War and Peace" },
                    { new Guid("bd9c80b5-138b-4ef5-a091-f9ca697cc260"), new Guid("8c98ef9a-b29b-46b1-bcca-e31b39c46568"), "A classic novel about manners and marriage.", "Romance", "https://images-eu.ssl-images-amazon.com/images/I/81NLDvyAHrL._AC_UL600_SR600,600_.jpg", true, "9781503290563", "Pride and Prejudice" },
                    { new Guid("e7275cc0-c33a-4023-a8ee-be028691bc76"), new Guid("37c2b95e-bf1b-41b9-bcca-10aef794e913"), "A novel about the adventures of a young boy along the Mississippi River.", "Fiction", "https://images-eu.ssl-images-amazon.com/images/I/91yLxd90MiL._AC_UL600_SR600,600_.jpg", true, "9780486280615", "Adventures of Huckleberry Finn" },
                    { new Guid("ea188a26-0fb4-4a81-a969-bf0e431eee29"), new Guid("a3e96cc7-f74a-4c9b-857a-7a8a4ff15d59"), "A novel about a dystopian future.", "Fiction", "https://m.media-amazon.com/images/I/71rpa1-kyvL._AC_UF894,1000_QL80_.jpg", true, "9780451524935", "1984" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookLoans_BookId",
                table: "BookLoans",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookLoans_UserId",
                table: "BookLoans",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_Token",
                table: "RefreshTokens",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookLoans");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
