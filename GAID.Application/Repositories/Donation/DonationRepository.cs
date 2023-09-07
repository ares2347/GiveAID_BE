using System.Linq.Expressions;
using GAID.Domain;
using GAID.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GAID.Application.Repositories.Donation;

public class DonationRepository : BaseRepository<Domain.Models.Donation.Donation>
{
    public DonationRepository(AppDbContext dbContext, UserContext userContext, UserManager<Domain.Models.User.User> userManager) : base(dbContext, userContext, userManager)
    {
    }

    public override IQueryable<Domain.Models.Donation.Donation> Get(
        Expression<Func<Domain.Models.Donation.Donation, bool>>? expression, int? size, int? page)
    {
        return base.Get(expression, size, page)
            .Include(x => x.Enrollment)
            .ThenInclude(y => y.Program)
            .ThenInclude(z => z.Partner)
            .Include(x => x.CreatedBy)
            .Include(x => x.ModifiedBy);
    }
    public override async Task<Domain.Models.Donation.Donation?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var res = await DbContext.Donations
            .Include(x => x.Enrollment)
            .ThenInclude(y => y.Program)
            .ThenInclude(z => z.Partner)
            .Include(x => x.CreatedBy)
            .Include(x => x.ModifiedBy)
            .FirstOrDefaultAsync(x => x.DonationId == id && !x.IsDelete,
                cancellationToken);
        return res;
    }
}