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
        IFilter<TEntity> filter,
        CancellationToken cancellationToken = default);

    Task<TDto?> GetAsync<TDto>(
        IFilter<TEntity> filter,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default
    );

    Task<TEntity?> GetAsync(
        IFilter<TEntity> filter,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default);

    Task<List<TDto>> GetListAsync<TDto>(
        IFilter<TEntity> filter,
        Expression<Func<TEntity, object>>? orderBy = null,
        bool descending = false,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default);

    Task<PagedList<TDto>> GetPagedListAsync<TDto>(
        int page,
        int pageSize,
        IFilter<TEntity> filter,
        Expression<Func<TEntity, object>>? orderBy = null,
        bool descending = false,
        bool asNoTracking = true,
        CancellationToken cancellationToken = default);
}
