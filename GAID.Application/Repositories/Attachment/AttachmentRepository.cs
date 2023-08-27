using System.Linq.Expressions;
using System.Net;
using GAID.Domain;
using GAID.Shared;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GAID.Application.Repositories.Attachment;

public class AttachmentRepository : BaseRepository<Domain.Models.Attachment.Attachment>
{
    private readonly UserContext _userContext;
    private readonly UserManager<Domain.Models.User.User> _userManager;

    public AttachmentRepository(AppDbContext dbContext, UserContext userContext, UserManager<Domain.Models.User.User> userManager) : base(dbContext, userContext, userManager)
    {
        _userContext = userContext;
        _userManager = userManager;
    }

    public override async Task<Domain.Models.Attachment.Attachment?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Attachments
            .Include(x => x.CreatedBy)
            .Include(x => x.ModifiedBy)
            .FirstOrDefaultAsync(a => a.AttachmentId.Equals(id),
            cancellationToken: cancellationToken);
    }

    public override async Task<Domain.Models.Attachment.Attachment> Create(Domain.Models.Attachment.Attachment entity)
    {
        if (entity.AttachmentId.Equals(Guid.Empty))
        {
            entity.AttachmentId = NewId.NextGuid();
        }
        var user = await _userManager.FindByIdAsync(_userContext.UserId.ToString());
        entity.CreatedBy = user;
        entity.CreatedAt = DateTimeOffset.UtcNow;
        var res = DbContext.Attachments.Add(entity);

        return res.Entity;
    }
    }