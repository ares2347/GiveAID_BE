using AutoMapper;
using GAID.Api.Dto;
using GAID.Api.Dto.Donation;
using GAID.Application.Repositories;
using GAID.Domain.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GAID.Api.Controllers;

[Route("api/admin/[controller]")]
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

    [HttpGet]
    public async Task<ActionResult<ListingResult<DonationAdminDto>>> GetDonations(int page = 0, int size = 10,
        CancellationToken _ = default)
    {
        var donations = _unitOfWork.DonationRepository.Get(null, size, page).Select(x => _mapper.Map<DonationAdminDto>(x));
        var total = _unitOfWork.DonationRepository.Count(null);
        return Ok(new ListingResult<DonationAdminDto>
        {
            Data = donations,
            Page = page,
            Size = size,
            Total = total
        });
    }
}