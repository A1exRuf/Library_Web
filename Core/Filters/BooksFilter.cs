using Core.Abstractions;
using Core.Entities;

namespace Core.Filters;

public class BooksFilter : IFilter<Book>
{
    public Guid? Id { get; set; }
    public string? SearchTerm { get; set; }
    public string?[]? Genres { get; set; }
    public Guid?[]? Authors { get; set; }
    public bool? ShowUnavailable { get; set; }



    public IQueryable<Book> Apply(IQueryable<Book> query)
    {
        if (Id != null)
        {
            query = query.Where(x => x.Id == Id);
        }
        else
        {
            if (ShowUnavailable != true)
            {
                query = query.Where(b =>
                b.IsAvailable == true);
            }

            if (Genres != null && Genres.Any())
            {
                query = query.Where(b =>
                Genres.Contains(b.Genree));
            }

            if (Authors != null && Authors.Any())
            {
                query = query.Where(b =>
                Authors.Contains(b.AuthorId));
            }

            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                string searhTerm = SearchTerm.ToLower();
                query = query.Where(b =>
                b.Isbn.Contains(searhTerm) ||
                b.Title.ToLower().Contains(searhTerm));
            }
        }

        return query;
    }
}
