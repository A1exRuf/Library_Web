using Core.Abstractions;
using Core.Entities;

namespace Core.Filters;

public class UsersFilter : IFilter<User>
{
    public Guid? Id { get; set; }
    public string? Email { get; set; }
    public string? SearchTerm { get; set; }

    public IQueryable<User> Apply(IQueryable<User> query)
    {
        if (Id != null)
        {
            query = query.Where(x => x.Id == Id);
        } 
        else if (Email != null)
        {
            query = query.Where(x => x.Email == Email);
        }
        else
        {
            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                query = query.Where(x =>
                x.Name.ToLower().Contains(SearchTerm.ToLower()));
            }
        }

        return query;
    }
}
