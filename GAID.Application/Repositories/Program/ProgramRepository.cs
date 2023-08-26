using GAID.Domain;
using Microsoft.EntityFrameworkCore;

namespace GAID.Application.Repositories.Program;

public class ProgramRepository : BaseRepository<Domain.Models.Program.Program>
{
    public ProgramRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<Domain.Models.Program.Program?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var res = await DbContext.Programs
            .Include(x => x.Enrollments)
            .Include(x => x.Page)
            .Include(x => x.CreatedBy)
            .Include(x => x.ModifiedBy)
            .FirstOrDefaultAsync(x => x.PartnerId == id && !x.IsDelete,
                cancellationToken);
        return res;
    }
}