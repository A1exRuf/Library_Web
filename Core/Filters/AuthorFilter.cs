using Core.Abstractions;
using Core.Entities;

namespace Core.Filters;

public class AuthorFilter : IFilter<Author>
{
    public Guid? Id { get; set; }

    public IQueryable<Author> Apply(IQueryable<Author> query)
    {
        if (Id != null)
        {
            query = query.Where(a => a.Id == Id);
        }
        return query;
    }
}
