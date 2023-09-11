using System.Linq.Expressions;
using GAID.Domain;
using GAID.Domain.Models.Donation;
using GAID.Domain.Models.User;
using Microsoft.EntityFrameworkCore;

namespace GAID.Application.Repositories.Admin;

public class AdminRepository
{
    private readonly AppDbContext _dbContext;

    public AdminRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> GetUsers(Expression<Func<User, bool>>? expression, CancellationToken _ = default)
    {
        var query = _dbContext.Users.AsQueryable();
        if (expression != null)
            query = query.Where(expression);
        return await query.CountAsync(x => !x.IsDeleted, _);
    }

    public async Task<int> GetActivities(Expression<Func<Domain.Models.Program.Program, bool>>? expression,
        CancellationToken _ = default)
    {
        var query = _dbContext.Programs.AsQueryable();
        if (expression != null)
            query = query.Where(expression);
        return await query.CountAsync(x => !x.IsDelete, _);
    }

    public async Task<decimal> GetDonations(Expression<Func<Domain.Models.Donation.Donation, bool>>? expression,
        CancellationToken _ = default)
    {
        var query = _dbContext.Donations.AsQueryable();
        if (expression != null)
            query = query.Where(expression);
        return await query.CountAsync(x => !x.IsDelete, _);
    }

    public async Task<Domain.Models.Program.Program?> GetMostDonation(CancellationToken _ = default)
    {
        var query = await _dbContext.Donations
            .GroupBy(x => x.Enrollment.Program)
            .Select(x => new
            {
                Program = x.Key,
                TotalDonations = x.Sum(donation => donation.Amount),
            })
            .OrderByDescending(x => x.TotalDonations)
            .FirstOrDefaultAsync(_);
        return await _dbContext.Programs.Include(x => x.Enrollments).ThenInclude(x => x.Donations)
            .FirstOrDefaultAsync(x => query != null && x.ProgramId == query.Program.ProgramId, _);
    }
}