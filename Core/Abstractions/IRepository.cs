using Core.Common;
using Core.Primitives;
using System.Linq.Expressions;

namespace Core.Abstractions;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default);

    Task<bool> RemoveByIdAsync(
        Guid id, 
        CancellationToken cancellationToken = default);

    void Update(TEntity entity);

    Task<bool> ExistsAsync(
        Expression<Func<TEntity, bool>> filter,
        CancellationToken cancellationToken = default);

    Task<TDto?> GetAsync<TDto>(
        Expression<Func<TEntity, bool>> filter,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default
    );

    Task<TEntity?> GetAsync(
        Expression<Func<TEntity, bool>> filter,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default);

    Task<List<TDto>> GetListAsync<TDto>(
        Expression<Func<TEntity, bool>>? filter = null,
        Expression<Func<TEntity, object>>? orderBy = null,
        bool descending = false,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default);

    Task<PagedList<TDto>> GetPagedListAsync<TDto, TFilter>(
        int page,
        int pageSize,
        TFilter filter,
        Expression<Func<TEntity, object>>? orderBy = null,
        bool descending = false,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default) where TFilter : IFilter<TEntity>;
}
