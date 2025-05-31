using Core.Abstractions;
using Core.Entities;

namespace Core.Filters;

public class BookLoanFilter : IFilter<BookLoan>
{
    public Guid? Id { get; set; }
    public Guid? UserId {  get; set; }

    public IQueryable<BookLoan> Apply(IQueryable<BookLoan> query)
    {
        if (Id != null)
        {
            query = query.Where(x => x.Id == Id);
        } 
        else
        {
            if (UserId != null)
            {
                query = query.Where(x => x.UserId == UserId);
            }
        }

        return query;
    }
}