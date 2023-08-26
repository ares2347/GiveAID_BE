using System.Linq.Expressions;
using System.Net;
using GAID.Domain;
using GAID.Shared;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace GAID.Application.Repositories.Attachment;

public class AttachmentRepository : BaseRepository<Domain.Models.Attachment.Attachment>
{

    public AttachmentRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
    
    public override async Task<Domain.Models.Attachment.Attachment?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Attachments
            .Include(x => x.CreatedBy)
            .Include(x => x.ModifiedBy)
            .FirstOrDefaultAsync(a => a.AttachmentId.Equals(id),
            cancellationToken: cancellationToken);
    }

    public override Domain.Models.Attachment.Attachment Create(Domain.Models.Attachment.Attachment entity)
    {
        if (entity.AttachmentId.Equals(Guid.Empty))
        {
            entity.AttachmentId = NewId.NextGuid();
        }

        var res = DbContext.Attachments.Add(entity);

        return res.Entity;
    }
    }