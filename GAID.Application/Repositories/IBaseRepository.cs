using System.Linq.Expressions;
using GAID.Domain.Models;

namespace GAID.Application.Repositories;

public interface IBaseRepository<T> where T : BaseEntity
{
    public IQueryable<T> Get(Expression<Func<T, bool>>? expression ,int? size, int? page);
    public Task<T?> GetById(Guid id, CancellationToken cancellationToken = default);
    public Task<T> Create(T entity);
    public Task<T> Update(T entity);
    public Task<bool> Delete(Guid id, bool isHardDelete = false, CancellationToken cancellationToken = default);
}