using System.Linq.Expressions;
using System.Net;
using GAID.Domain;
using GAID.Shared;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace GAID.Application.Repositories.Attachment;

public class AttachmentRepository : IBaseRepository<Domain.Models.Attachment.Attachment>
{
    private readonly AppDbContext _dbContext;

    public AttachmentRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<Domain.Models.Attachment.Attachment> Get(Expression<Func<Domain.Models.Attachment.Attachment, bool>>? expression = null, int size = 10,
        int page = 0)
    {
        var query = _dbContext.Attachments.Where(x => !x.IsDelete);
        if (expression is not null)
        {
            query = query.Where(expression);
        }

        return query.Skip(page).Take(size);
    }

    public async Task<Domain.Models.Attachment.Attachment?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Attachments.FirstOrDefaultAsync(a => a.AttachmentId.Equals(id),
            cancellationToken: cancellationToken);
    }

    public async Task<Domain.Models.Attachment.Attachment?> Create(Domain.Models.Attachment.Attachment entity, CancellationToken cancellationToken = default)
    {
        if (entity.AttachmentId.Equals(Guid.Empty))
        {
            entity.AttachmentId = NewId.NextGuid();
        }

        var res = _dbContext.Attachments.Add(entity);

        return res.Entity;
    }

    public Task<Domain.Models.Attachment.Attachment?> Update(Domain.Models.Attachment.Attachment entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Delete(Guid id, bool isHardDelete = false, CancellationToken cancellationToken = default)
    {
        var attachment = await _dbContext.Attachments.FirstOrDefaultAsync(a => a.AttachmentId.Equals(id),
            cancellationToken: cancellationToken);

        HttpException.ThrowIfNull(attachment, HttpStatusCode.NotFound);

        attachment!.IsDelete = true;
        _dbContext.Attachments.Update(attachment);
        if (isHardDelete)
        {
            _dbContext.Remove(attachment);
        }

        return true;
    }
}