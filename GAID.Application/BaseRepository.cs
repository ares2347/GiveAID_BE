using System.Linq.Expressions;
using GAID.Application.Repositories;
using GAID.Domain;
using GAID.Domain.Models;

namespace GAID.Application;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext DbContext;

    protected BaseRepository(AppDbContext dbContext)
    {
        DbContext = dbContext;
    }
    public virtual IQueryable<T> Get(Expression<Func<T, bool>>? expression, int? size, int? page)
    {
        var query = DbContext.Set<T>().AsQueryable();
        if (expression is not null)
        {
            query = query.Where(expression);
        }

        if (size is not null)
        {
            if (page is not null)
            {
                query = query.Skip(page.Value * size.Value).Take(size.Value);
            }
            else
            {
                query = query.Take(size.Value);
            }
        }

        return query;
    }

    public abstract Task<T?> GetById(Guid id, CancellationToken cancellationToken = default);

    public virtual T Create(T entity)
    {
        var res = DbContext.Set<T>().Add(entity);
        return res.Entity;
    }

    public virtual T Update(T entity)
    {
        entity.ModifiedAt = DateTimeOffset.UtcNow;
        var res = DbContext.Set<T>().Update(entity);
        return res.Entity;
    }

    public virtual async Task<bool> Delete(Guid id, bool isHardDelete = false, CancellationToken cancellationToken = default)
    {
        var res = await GetById(id, cancellationToken);
        if (res is not null)
        {
            res.IsDelete = true;
            res.ModifiedAt = DateTimeOffset.UtcNow;
            if (isHardDelete)
                DbContext.Set<T>().Remove(res);
            Update(res);
            return true;
        }

        return false;
    }
}