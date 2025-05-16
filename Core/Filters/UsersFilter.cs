using Core.Abstractions;
using Core.Entities;

namespace Core.Filters;

public class UsersFilter : IFilter<User>
{
    public string? SearchTerm { get; set; }

    public IQueryable<User> Apply(IQueryable<User> query)
    {
        if (!string.IsNullOrWhiteSpace(SearchTerm))
        {
            string searhTerm = SearchTerm.ToLower();
            query = query.Where(u =>
            u.Name.ToLower().Contains(searhTerm));
        }

        return query;
    }
}
