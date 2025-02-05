using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

internal static class ApplicationDbContextSeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        var author1Id = new Guid("62e21ba9-9377-44e7-9043-85b4eec3b0a5");
        var author2Id = new Guid("a3e96cc7-f74a-4c9b-857a-7a8a4ff15d59");
        var author3Id = new Guid("3a8f8be2-5c8e-4f9d-a20a-e788b01b1bd8");
        var author4Id = new Guid("3f965ba7-3962-4c5e-8b0b-67325256d865");
        var author5Id = new Guid("8c98ef9a-b29b-46b1-bcca-e31b39c46568");
        var author6Id = new Guid("37c2b95e-bf1b-41b9-bcca-10aef794e913");
        var author7Id = new Guid("5a95136f-a3f0-4bdf-8ab0-3996999a789a");
        var author8Id = new Guid("6473ccb9-f849-4946-b044-8d261a10cf0a");
        var author9Id = new Guid("e91bde0b-cd01-4c52-8245-f6278fe7b9a4");
        var author10Id = new Guid("49d08119-1428-4b47-b1b6-ec9077cfb877");
        var author11Id = new Guid("cea9d38a-1119-47b1-8ae8-c6b3a72d919e");
        var author12Id = new Guid("63ccc0b6-a595-4d38-92a8-2db150073698");

        var book1Id = new Guid("423ec25c-2c07-4c36-a95f-d38669dcc590");
        var book2Id = new Guid("ea188a26-0fb4-4a81-a969-bf0e431eee29");
        var book3Id = new Guid("1cc33180-bac0-4a7d-8894-c3596ea3c004");
        var book4Id = new Guid("a2e79b1c-a9fa-46ec-955e-74c893dc49e1");
        var book5Id = new Guid("bd9c80b5-138b-4ef5-a091-f9ca697cc260");
        var book6Id = new Guid("e7275cc0-c33a-4023-a8ee-be028691bc76");
        var book7Id = new Guid("b4d899ce-ae21-459c-8ca9-cf25f91e07b7");
        var book8Id = new Guid("65a4b373-6096-4b7f-bf51-28196f42d34c");
        var book9Id = new Guid("0c93d826-eed6-4e8d-ad55-11bd4a8d0f33");
        var book10Id = new Guid("525bdebf-83c8-49c4-b5cd-013c327cc3e2");
        var book11Id = new Guid("3593d89b-6c57-4f94-a8e5-09129d913701");
        var book12Id = new Guid("97d6a6d4-9096-4380-8e10-a35a8568fcc9");

        var authors = new List<Author>
        {
            new (author1Id, "J.K.", "Rowling", new DateTime(1965, 7, 31, 0, 0, 0, DateTimeKind.Utc), "United Kingdom"),
            new (author2Id, "George", "Orwell", new DateTime(1903, 6, 25, 0, 0, 0, DateTimeKind.Utc), "United Kingdom"),
            new (author3Id, "F. Scott", "Fitzgerald", new DateTime(1896, 9, 24, 0, 0, 0, DateTimeKind.Utc), "United States"),
            new (author4Id, "Harper", "Lee", new DateTime(1926, 4, 28, 0, 0, 0, DateTimeKind.Utc), "United States"),
            new (author5Id, "Jane", "Austen", new DateTime(1775, 12, 16, 0, 0, 0, DateTimeKind.Utc), "United Kingdom"),
            new (author6Id, "Mark", "Twain", new DateTime(1835, 11, 30, 0, 0, 0, DateTimeKind.Utc), "United States"),
            new (author7Id, "Leo", "Tolstoy", new DateTime(1828, 9, 9, 0, 0, 0, DateTimeKind.Utc), "Russia"),
            new (author8Id, "Gabriel", "Garcia Marquez", new DateTime(1927, 3, 6, 0, 0, 0, DateTimeKind.Utc), "Colombia"),
            new (author9Id, "Herman", "Melville", new DateTime(1819, 8, 1, 0, 0, 0, DateTimeKind.Utc), "United States"),
            new (author10Id, "Mary", "Shelley", new DateTime(1797, 8, 30, 0, 0, 0, DateTimeKind.Utc), "United Kingdom"),
            new (author11Id, "J.R.R.", "Tolkien", new DateTime(1892, 1, 3, 0, 0, 0, DateTimeKind.Utc), "United Kingdom"),
            new (author12Id, "Ernest", "Hemingway", new DateTime(1899, 7, 21, 0, 0, 0, DateTimeKind.Utc), "United States")
        };

        var books = new List<Book>
        {
            new (book1Id, "9781408855652", "Harry Potter and the Philosopher's Stone", "Fantasy", "The book that started it all.", author1Id, "https://images-na.ssl-images-amazon.com/images/I/51UoqRAxwEL._SX331_BO1,204,203,200_.jpg"),
            new (book2Id, "9780451524935", "1984", "Fiction", "A novel about a dystopian future.", author2Id, "https://m.media-amazon.com/images/I/71rpa1-kyvL._AC_UF894,1000_QL80_.jpg"),
            new (book3Id, "9780743273565", "The Great Gatsby", "Romance", "A story of the Jazz Age in the United States.", author3Id, "https://m.media-amazon.com/images/I/81TLiZrasVL._AC_UF1000,1000_QL80_.jpg"),
            new (book4Id, "9780061120084", "To Kill a Mockingbird", "Fiction", "A novel about racial injustice in the Deep South.", author4Id, "https://m.media-amazon.com/images/I/81JYG8sqRsL._AC_UF1000,1000_QL80_.jpg"),
            new (book5Id, "9781503290563", "Pride and Prejudice", "Romance", "A classic novel about manners and marriage.", author5Id, "https://cdn11.bigcommerce.com/s-gjd9kmzlkz/products/25092/images/24834/pride_9781503290563__49747.1607045952.386.513.jpg?c=1"),
            new (book6Id, "9780486280615", "Adventures of Huckleberry Finn", "Fiction", "A novel about the adventures of a young boy along the Mississippi River.", author6Id, "https://dover-books-us.imgix.net/covers/9780486280615.jpg?auto=format&w=300"),
            new (book7Id, "9780199232765", "War and Peace", "Fiction", "A historical novel set during the Napoleonic Wars.", author7Id, "https://blackwells.co.uk/jacket/l/9780199232765.webp"),
            new (book8Id, "9781400034934", "One Hundred Years of Solitude", "Fiction", "A landmark novel in Latin American literature.", author8Id, "https://m.media-amazon.com/images/I/71WQQjgEhQL.jpg"),
            new (book9Id, "9781503280786", "Moby-Dick", "Fiction", "A novel about Captain Ahab's obsession with a great white whale.", author9Id, "https://images.booksense.com/images/786/280/9781503280786.jpg"),
            new (book10Id, "9780486282114", "Frankenstein", "Science Fiction", "A Gothic novel about a scientist who creates life.", author10Id, "https://pictures.abebooks.com/isbn/9780486282114-uk.jpg"),
            new (book11Id, "9780618640157", "The Lord of the Rings", "Fantasy", "An epic high-fantasy novel.", author11Id, "https://pictures.abebooks.com/isbn/9780618640157-uk.jpg"),
            new (book12Id, "9780684801469", "The Old Man and the Sea", "Fiction", "A novel about an aging fisherman's battle with a giant marlin.", author12Id, "https://m.media-amazon.com/images/I/71+rqbp57dL._AC_UF894,1000_QL80_.jpg")
        };

        modelBuilder.Entity<Author>().HasData(authors);
        modelBuilder.Entity<Book>().HasData(books);
    }
}
