using System.Linq.Expressions;
using GAID.Domain;
using GAID.Domain.Models.Enrollment;
using GAID.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GAID.Application.Repositories.Program;

public class ProgramRepository : BaseRepository<Domain.Models.Program.Program>
{
    private readonly UserContext _userContext;
    private readonly UserManager<Domain.Models.User.User> _userManager;

    public override IQueryable<Domain.Models.Program.Program> Get(Expression<Func<Domain.Models.Program.Program, bool>>? expression, int? size, int? page)
    {
        return base.Get(expression, size, page).Include(x => x.ProgramThumbnail);
    }

    public override async Task<Domain.Models.Program.Program?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var res = await DbContext.Programs
            .Include(x => x.Partner)
            .Include(x => x.Enrollments)
            .ThenInclude(y => y.Donations)
            .Include(x => x.Page)
            .Include(x => x.CreatedBy)
            .Include(x => x.ModifiedBy)
            .FirstOrDefaultAsync(x => x.ProgramId == id && !x.IsDelete,
                cancellationToken);
        if (res is not null)
            res.CurrentUserEnrollment = res.Enrollments.FirstOrDefault(x => x.CreatedBy is not null && x.CreatedBy.Id == _userContext.UserId);
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
    
    public async Task<Domain.Models.Program.Program?> AddDonation(Guid programId, Domain.Models.Donation.Donation donation, CancellationToken cancellationToken)
    {
        var program = await GetById(programId, cancellationToken);
        var enrollment = program?.Enrollments.FirstOrDefault(x => x.CreatedBy is not null && x.CreatedBy.Id == _userContext.UserId);
        donation.CreatedBy = await _userManager.FindByIdAsync(_userContext.UserId.ToString());
        donation.CreatedAt = DateTimeOffset.UtcNow;
        enrollment?.Donations.Add(donation);
        return program;
    }

    public async Task CloseProgramDueDate()
    {
        var duePrograms = await DbContext.Programs
            .Where(x => x.EndDate < DateOnly.FromDateTime(DateTimeOffset.UtcNow.DateTime) && !x.IsDelete).ToListAsync();
        foreach (var program in duePrograms)
        {
            if (program.IsClosed) continue;
            program.IsClosed = true;
            program.ClosedReason = "Program has been closed by end date.";
        }
        DbContext.Programs.UpdateRange(duePrograms);
        await DbContext.SaveChangesAsync();
    }
    public ProgramRepository(AppDbContext dbContext, UserContext userContext, UserManager<Domain.Models.User.User> userManager) : base(dbContext, userContext, userManager)
    {
        _userContext = userContext;
        _userManager = userManager;
    }
    
}