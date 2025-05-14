using Core.Primitives;
using System.Linq.Expressions;

namespace Core.Abstractions;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task<TDto?> GetAsync<TDto>(
        Expression<Func<TEntity, bool>> filter,
        bool asNoTracking,
        CancellationToken cancellationToken = default
    );
}
