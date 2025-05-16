using Core.Primitives;

namespace Core.Abstractions;

public interface IFilter<TEntity> where TEntity : Entity
{
    IQueryable<TEntity> Apply(IQueryable<TEntity> query);
}
