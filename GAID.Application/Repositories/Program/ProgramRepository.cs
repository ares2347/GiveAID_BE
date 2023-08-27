using GAID.Domain;
using GAID.Domain.Models.Donation;
using GAID.Domain.Models.Enrollment;
using GAID.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GAID.Application.Repositories.Program;

public class ProgramRepository : BaseRepository<Domain.Models.Program.Program>
{
    private readonly UserContext _userContext;
    private readonly UserManager<Domain.Models.User.User> _userManager;

    public override async Task<Domain.Models.Program.Program?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var res = await DbContext.Programs
            .Include(x => x.Enrollments)
            .ThenInclude(y => y.Donations)
            .Include(x => x.Page)
            .Include(x => x.CreatedBy)
            .Include(x => x.ModifiedBy)
            .FirstOrDefaultAsync(x => x.PartnerId == id && !x.IsDelete,
                cancellationToken);
        return res;
    }

    public async Task<Domain.Models.Program.Program?> AddEnrollment(Guid id,
        CancellationToken cancellationToken = default)
    {
        var program = await GetById(id, cancellationToken);
        program?.Enrollments.Add(new Enrollment()
        {
            CreatedBy = await _userManager.FindByIdAsync(_userContext.UserId.ToString()),
            CreatedAt = DateTimeOffset.UtcNow
        });

        return program;
    }
    
    public async Task<Domain.Models.Program.Program?> AddDonation(Guid programId, Donation donation, CancellationToken cancellationToken)
    {
        var program = await GetById(programId, cancellationToken);
        var enrollment = program?.Enrollments.FirstOrDefault(x => x.CreatedBy is not null && x.CreatedBy.Id == _userContext.UserId);
        donation.CreatedBy = await _userManager.FindByIdAsync(_userContext.UserId.ToString());
        donation.CreatedAt = DateTimeOffset.UtcNow;
        enrollment?.Donations.Add(donation);
        return program;
    }
    
    public ProgramRepository(AppDbContext dbContext, UserContext userContext, UserManager<Domain.Models.User.User> userManager) : base(dbContext, userContext, userManager)
    {
        _userContext = userContext;
        _userManager = userManager;
    }
    
}