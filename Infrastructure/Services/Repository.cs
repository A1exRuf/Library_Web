using Core.Abstractions;
using Core.Common;
using Core.Primitives;
using Mapster;
using Microsoft.EntityFrameworkCore;
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

    public Task<bool> ExistsAsync(
        Expression<Func<TEntity, bool>> filter, 
        CancellationToken cancellationToken = default)
    {
        return _context.Set<TEntity>()
            .Where(filter)
            .AnyAsync(cancellationToken);
    }

    public async Task<TDto?> GetAsync<TDto>(
        Expression<Func<TEntity, bool>> filter,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Set<TEntity>().AsQueryable();

        query = AddAsNoTracking(asNoTracking, query);

        query = FilterQuery(filter, query);

        var data = await query
            .ProjectToType<TDto>()
            .FirstOrDefaultAsync(cancellationToken);

        return data;
    }

    public async Task<TEntity?> GetAsync(
        Expression<Func<TEntity, bool>> filter,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Set<TEntity>().AsQueryable();

        query = AddAsNoTracking(asNoTracking, query);

        query = FilterQuery(filter, query);

        var data = await query.FirstOrDefaultAsync(cancellationToken);

        return data;
    }

    public async Task<List<TDto>> GetListAsync<TDto>(
        Expression<Func<TEntity, bool>>? filter,
        Expression<Func<TEntity, object>>? orderBy = null,
        bool descending = false,
        bool asNoTracking = true, 
        CancellationToken cancellationToken = default)
    {
        var query = _context.Set<TEntity>().AsQueryable();

        query = FilterQuery(filter, query);

        query = SortQuery(orderBy, descending, query);

        query = AddAsNoTracking(asNoTracking, query);

        var queryDTO = query.ProjectToType<TDto>();

        var items = await queryDTO.ToListAsync(cancellationToken);

        return items;
    }

    public async Task<PagedList<TDto>> GetPagedListAsync<TDto, TFilter>(
        int page,
        int pageSize,
        TFilter filter,
        Expression<Func<TEntity, object>>? orderBy = null,
        bool descending = false,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default) where TFilter : IFilter<TEntity>
    {
        var query = _context.Set<TEntity>().AsQueryable();

        query = filter.Apply(query);

        query = SortQuery(orderBy, descending, query);

        query = AddAsNoTracking(asNoTracking, query);

        var queryDTO = query.ProjectToType<TDto>();

        int totalCount = await query.CountAsync(cancellationToken);

        var items = await queryDTO
            .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

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

    private IQueryable<TEntity> FilterQuery(Expression<Func<TEntity, bool>>? filter, IQueryable<TEntity> query)
    {
        if (filter != null)
        {
            query = query.Where(filter);
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