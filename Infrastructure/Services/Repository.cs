using Core.Abstractions;
using Core.Primitives;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Services;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    private readonly ApplicationDbContext _context;

    public Repository(ApplicationDbContext context) => _context = context;

    public async Task<TDto?> GetAsync<TDto>(
        Expression<Func<TEntity, bool>> filter,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Set<TEntity>();

        if (asNoTracking)
        {
            query.AsNoTracking();
        }

        var data = await query.Where(filter).ProjectToType<TDto>().FirstOrDefaultAsync(cancellationToken);

        return data;
    }
}