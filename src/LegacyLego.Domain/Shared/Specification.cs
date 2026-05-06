using System.Linq.Expressions;

namespace LegacyLego.Domain.Shared;

public abstract class Specification<TEntity, TId, TResult> : Specification<TEntity, TId>
    where TEntity : Entity<TId>
    where TId : ValueObject
{
    public Expression<Func<TEntity, TResult>> Selector { get; }

    protected Specification(
        Expression<Func<TEntity, TResult>> selector)
    {
        Selector = selector;
    }
}

public abstract class Specification<TEntity, TId>
    where TEntity : Entity<TId>
    where TId : ValueObject
{
    public int? SkipNum { get; private set; }

    public int? LimitNum { get; private set; }

    public List<Expression<Func<TEntity, bool>>> FilterExpressions { get; } = new();

    public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = new();

    public List<Expression<Func<TEntity, object>>> OrderByExpressions { get; } = new();

    public List<Expression<Func<TEntity, object>>> OrderByDescendingExpressions { get; } = new();

    protected Specification() { }

    protected void AddFilter(Expression<Func<TEntity, bool>> filterExpression) =>
        FilterExpressions.Add(filterExpression);

    protected void AddInclude(Expression<Func<TEntity, object>> includeExpression) =>
        IncludeExpressions.Add(includeExpression);

    protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression) =>
        OrderByExpressions.Add(orderByExpression);

    protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression) =>
        OrderByDescendingExpressions.Add(orderByDescendingExpression);

    protected void SetSkipNum(int skipNum) =>
        SkipNum = skipNum;

    protected void SetLimitNum(int limitNum) =>
        LimitNum = limitNum;

    protected void DropSkip() =>
        SkipNum = null;

    protected void DropLimit() =>
        LimitNum = null;
}