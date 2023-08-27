using System.Linq.Expressions;
using GAID.Domain;
using GAID.Domain.Models;
using GAID.Shared;
using Microsoft.AspNetCore.Identity;

namespace GAID.Application.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext DbContext;
    private readonly UserContext _userContext;
    private readonly UserManager<Domain.Models.User.User> _userManager;

    protected BaseRepository(AppDbContext dbContext, UserContext userContext, UserManager<Domain.Models.User.User> userManager)
    {
        DbContext = dbContext;
        _userContext = userContext;
        _userManager = userManager;
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

    public virtual async Task<T> Create(T entity)
    {
        var user = await _userManager.FindByIdAsync(_userContext.UserId.ToString());
        entity.CreatedBy = user;
        entity.CreatedAt = DateTimeOffset.UtcNow;
        var res = DbContext.Set<T>().Add(entity);
        return res.Entity;
    }

    public virtual async Task<T> Update(T entity)
    {
        var user = await _userManager.FindByIdAsync(_userContext.UserId.ToString());
        entity.ModifiedBy = user;
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