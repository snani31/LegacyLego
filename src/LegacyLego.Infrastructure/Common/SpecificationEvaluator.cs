using LegacyLego.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace LegacyLego.Infrastructure.Common;

internal static class SpecificationEvaluator
{
    internal static IQueryable<TResult> GetQuery<TEntity, TId, TResult>(
        IQueryable<TEntity> inputQueryable,
        Specification<TEntity, TId, TResult> specification)
        where TEntity : Entity<TId>
        where TId : ValueObject
    {
        var queryable = ApplyBaseSpecifications(inputQueryable, specification);

        return queryable.Select(specification.Selector);
    }

    internal static IQueryable<TEntity> GetQuery<TEntity, TId>(
        IQueryable<TEntity> inputQueryable,
        Specification<TEntity, TId> specification)
        where TEntity : Entity<TId>
        where TId : ValueObject
    {
        var queryable = ApplyBaseSpecifications(inputQueryable, specification);

        foreach (var exp in specification.IncludeExpressions)
            queryable = queryable.Include(exp);

        return queryable;
    }

    private static IQueryable<TEntity> ApplyBaseSpecifications<TEntity, TId>(
        IQueryable<TEntity> inputQueryable,
        Specification<TEntity, TId> specification)
        where TEntity : Entity<TId>
        where TId : ValueObject
    {
        IQueryable<TEntity> queryable = inputQueryable;

        foreach (var exp in specification.FilterExpressions)
            queryable = queryable.Where(exp);

        if (specification.OrderByExpressions.Any())
        {
            var ordered = queryable.OrderBy(specification.OrderByExpressions[0]);
            for (int i = 1; i < specification.OrderByExpressions.Count; i++)
                ordered = ordered.ThenBy(specification.OrderByExpressions[i]);
            queryable = ordered;
        }

        if (specification.OrderByDescendingExpressions.Any())
        {
            var ordered = queryable.OrderByDescending(specification.OrderByDescendingExpressions[0]);
            for (int i = 1; i < specification.OrderByDescendingExpressions.Count; i++)
                ordered = ordered.ThenByDescending(specification.OrderByDescendingExpressions[i]);
            queryable = ordered;
        }

        if (specification.SkipNum.HasValue)
            queryable = queryable.Skip(specification.SkipNum.Value);

        if (specification.LimitNum.HasValue)
            queryable = queryable.Take(specification.LimitNum.Value);

        return queryable;
    }
}