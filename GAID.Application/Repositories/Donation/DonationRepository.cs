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