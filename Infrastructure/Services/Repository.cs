using Core.Abstractions;
using Core.Common;
using Core.Primitives;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.Services;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    private readonly ApplicationDbContext _context;

    public Repository(ApplicationDbContext context) => _context = context;

    public async Task AddAsync(
        TEntity entity, 
        CancellationToken cancellationToken = default)
    {
        await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    public async Task<bool> RemoveByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        TEntity? entity = await _context
            .Set<TEntity>()
            .FindAsync(id, cancellationToken);

        if (entity == null) return false;

        _context.Set<TEntity>().Remove(entity);

        return true;
    }

    public void Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
    }

    public async Task<bool> ExistsAsync(
        IFilter<TEntity> filter,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Set<TEntity>().AsQueryable();
        query = filter.Apply(query);

        var result = await query.AnyAsync(cancellationToken);

        return result;
    }

    public async Task<TDto?> GetAsync<TDto>(
        IFilter<TEntity> filter,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Set<TEntity>().AsQueryable();
        query = AddAsNoTracking(asNoTracking, query);
        query = filter.Apply(query);

        var data = await query
            .ProjectToType<TDto>()
            .FirstOrDefaultAsync(cancellationToken);

        return data;
    }

    public async Task<TEntity?> GetAsync(
        IFilter<TEntity> filter,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Set<TEntity>().AsQueryable();
        query = AddAsNoTracking(asNoTracking, query);
        query = filter.Apply(query);

        var data = await query.FirstOrDefaultAsync(cancellationToken);

        return data;
    }

    public async Task<List<TDto>> GetListAsync<TDto>(
        IFilter<TEntity> filter,
        Expression<Func<TEntity, object>>? orderBy = null,
        bool descending = false,
        bool asNoTracking = true, 
        CancellationToken cancellationToken = default)
    {
        var query = _context.Set<TEntity>().AsQueryable();
        query = AddAsNoTracking(asNoTracking, query);
        query = filter.Apply(query);
        query = SortQuery(orderBy, descending, query);

        var items = await query
            .ProjectToType<TDto>()
            .ToListAsync(cancellationToken);

        return items;
    }

    public async Task<PagedList<TDto>> GetPagedListAsync<TDto>(
        int page,
        int pageSize,
        IFilter<TEntity> filter,
        Expression<Func<TEntity, object>>? orderBy = null,
        bool descending = false,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Set<TEntity>().AsQueryable();
        query = filter.Apply(query);
        query = SortQuery(orderBy, descending, query);
        query = AddAsNoTracking(asNoTracking, query);

        int totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .ProjectToType<TDto>()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new(items, page, pageSize, totalCount);
    }

    private IQueryable<TEntity> SortQuery<TSort>(Expression<Func<TEntity, TSort>>? orderBy, bool descending, IQueryable<TEntity> query)
    {
        if (orderBy != null)
        {
            if (descending)
                query = query.OrderByDescending(orderBy);
            else
                query = query.OrderBy(orderBy);
        }
        else
        {
            query = query.OrderBy(x => x.Id);
        }

        return query;
    }

    private IQueryable<TEntity> AddAsNoTracking(bool asNoTracking, IQueryable<TEntity> query)
    {
        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }
}