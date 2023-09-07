using GAID.Domain;
using GAID.Domain.Models.Page;
using GAID.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GAID.Application.Repositories.Page;

public class PageRepository : BaseRepository<Domain.Models.Page.Page>
{
    
    public override async Task<Domain.Models.Page.Page?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Pages
            .Include(x => x.CreatedBy)
            .Include(x => x.ModifiedBy)
            .FirstOrDefaultAsync(a => a.PageId.Equals(id),
                cancellationToken: cancellationToken);
    }    
    
    public async Task<Domain.Models.Page.Page?> GetByType(PageType type, Guid? partnerId, Guid? programId, CancellationToken cancellationToken = default)
    {
        return await DbContext.Pages
            .Include(x => x.CreatedBy)
            .Include(x => x.ModifiedBy)
            .FirstOrDefaultAsync(a => a.PageType == type && a.PartnerId == partnerId && a.ProgramId == programId,
                cancellationToken: cancellationToken);
    }
    
    public PageRepository(AppDbContext dbContext, UserContext userContext, UserManager<Domain.Models.User.User> userManager) : base(dbContext, userContext, userManager)
    {
    }
}