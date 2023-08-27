using System.Linq.Expressions;
using GAID.Domain;
using GAID.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GAID.Application.Repositories.Partner;

public class PartnerRepository : BaseRepository<Domain.Models.Partner.Partner>
{
    public override async Task<Domain.Models.Partner.Partner?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var res = await DbContext.Partners
            .Include(x => x.Page)
            .Include(x => x.Programs)
            .Include(x => x.CreatedBy)
            .Include(x => x.ModifiedBy)
            .FirstOrDefaultAsync(x => x.PartnerId == id && !x.IsDelete,
            cancellationToken);
        return res;
    }

    public PartnerRepository(AppDbContext dbContext, UserContext userContext, UserManager<Domain.Models.User.User> userManager) : base(dbContext, userContext, userManager)
    {
    }
}