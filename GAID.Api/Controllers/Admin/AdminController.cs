using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using GAID.Api.Dto;
using GAID.Api.Dto.Admin;
using GAID.Api.Dto.Donation;
using GAID.Application.Repositories;
using GAID.Domain.Models.Donation;
using GAID.Domain.Models.User;
using MassTransit.Initializers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace GAID.Api.Controllers.Admin;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = RoleName.Admin)]
public class AdminController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AdminController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet("donation")]
    public async Task<ActionResult<ListingResult<DonationAdminDto>>> GetDonations(int page = 0, int size = 10,
        CancellationToken _ = default)
    {
        var donations = _unitOfWork.DonationRepository.Get(null, size, page)
            .Select(x => _mapper.Map<DonationAdminDto>(x));
        var total = _unitOfWork.DonationRepository.Count(null);
        return Ok(new ListingResult<DonationAdminDto>
        {
            Data = donations,
            Page = page,
            Size = size,
            Total = total
        });
    }

    [HttpGet("dashboard")]
    public async Task<ActionResult<AdminDashboardDto>> GetDashboard(CancellationToken _ = default)
    {
        try
        {
            var totalUsers = await _unitOfWork.AdminRepository.GetUsers(null, _);
            var newUsersMonth =
                await _unitOfWork.AdminRepository.GetUsers(x => x.CreatedAt.Month == DateTimeOffset.UtcNow.Month, _);
            var totalActivities = await _unitOfWork.AdminRepository.GetActivities(x => x.IsOpen && !x.IsClosed, _);
            var newActivitiesMonth = await _unitOfWork.AdminRepository.GetActivities(
                x => x.CreatedAt != null && x.IsOpen && !x.IsClosed &&
                     x.CreatedAt.Value.Month == DateTimeOffset.UtcNow.Month, _);
            var totalDonations =
                await _unitOfWork.AdminRepository.GetDonations(x => x.Status == DonationStatus.Completed, _);
            var totalDonationsMonth = await _unitOfWork.AdminRepository.GetDonations(
                x => x.CreatedAt != null && x.Status == DonationStatus.Completed &&
                     x.CreatedAt.Value.Month == DateTimeOffset.UtcNow.Month, _);
            var mostRegistered = await _unitOfWork.ProgramRepository.Get(x => !x.IsDelete, 1, 0)
                .Select(x => new RegistrationBrief
                {
                    Id = x.ProgramId,
                    Title = x.Name,
                    Registered = x.Enrollments.Count()
                })
                .OrderByDescending(x => x.Registered).FirstOrDefaultAsync(_);
            var mostDonated = await _unitOfWork.AdminRepository.GetMostDonation(_).Select(x => new DonationBrief
            {
                Id = x?.ProgramId ?? Guid.Empty,
                Title = x?.Name ?? string.Empty,
                Donations = x?.Enrollments.Sum(enrollment => enrollment.Donations.Sum(y => y.Amount)) ?? 0
            });
            var mostPartners = await _unitOfWork.PartnerRepository.Get(x => !x.IsDelete, null, null)
                .Select(x => new PartnerBrief
                {
                    Id = x.PartnerId,
                    Title = x.Name,
                    Programs = x.Programs.Count()
                })
                .OrderByDescending(x => x.Programs).FirstOrDefaultAsync(_);
            return Ok(new AdminDashboardDto
            {
                General =
                {
                    TotalActivities = totalActivities,
                    TotalDonations = totalDonations,
                    TotalUser = totalUsers
                },
                Time =
                {
                    NewActivitiesMonth = newActivitiesMonth,
                    NewUsersMonth = newUsersMonth,
                    TotalDonationsMonth = totalDonationsMonth
                },
                Activity =
                {
                    MostDonated = mostDonated ?? new(),
                    MostPartners = mostPartners ?? new(),
                    MostRegistered = mostRegistered ?? new()
                }
            });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}