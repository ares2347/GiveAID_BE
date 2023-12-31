using AutoMapper;
using GAID.Api.Dto;
using GAID.Api.Dto.Partner.Request;
using GAID.Api.Dto.Partner.Response;
using GAID.Application.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GAID.Api.Controllers.Partner;

[Route("api/[controller]")]
[ApiController]
public class PartnerController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PartnerController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    [HttpGet]
    public ActionResult<ListingResult<PartnerListingDto>> GetPartners(int page = 0, int size = 10, CancellationToken _ = default)
    {
        var partners = _unitOfWork.PartnerRepository.Get(x => !x.IsDelete, size, page)
            .Select(x => _mapper.Map<PartnerListingDto>(x));
        var total = _unitOfWork.PartnerRepository.Count(x => !x.IsDelete);
        return Ok(new ListingResult<PartnerListingDto>
        {
            Data = partners,
            Page = page,
            Size = size,
            Total = total
        });
    }

    [HttpGet("{partnerId:guid}")]
    public async Task<ActionResult<PartnerDetailDto>> GetPartnerById([FromRoute] Guid partnerId, CancellationToken _ = default)
    {
        try
        {
            var partner = await _unitOfWork.PartnerRepository.GetById(partnerId, _);
            if (partner is null) return NotFound();
            var result = _mapper.Map<PartnerDetailDto>(partner);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    //TODO: Add role to authorization
    [Authorize]
    [HttpPost("create")]
    public async Task<ActionResult<PartnerDetailDto>> CreatePartner(PartnerDetailRequest request, CancellationToken _ = default)
    {
        try
        {
            var entity = _mapper.Map<Domain.Models.Partner.Partner>(request);
            var attachment = await _unitOfWork.AttachmentRepository.GetById(request.PartnerThumbnailId, _);
            if(attachment is not null) entity.PartnerThumbnail = attachment;
            var result = await _unitOfWork.PartnerRepository.Create(entity);
            await _unitOfWork.SaveChangesAsync(_);
            return Ok(_mapper.Map<PartnerDetailDto>(result));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    //TODO: Add role to authorization
    [Authorize]
    [HttpPut("update/{partnerId:guid}")]
    public async Task<ActionResult<PartnerDetailDto>> UpdatePartner([FromRoute] Guid partnerId, PartnerDetailRequest request, CancellationToken _ = default)
    {
        try
        {
            var partner = await _unitOfWork.PartnerRepository.GetById(partnerId, _);
            if (partner is null) return NotFound();
            partner.Name = request.Name;
            partner.Email = request.Email;
            partner.Description = request.Description;
            // partner.Page = request.Page;
            var attachment = await _unitOfWork.AttachmentRepository.GetById(request.PartnerThumbnailId, _);
            if(attachment is not null) partner.PartnerThumbnail = attachment;
            var result = await _unitOfWork.PartnerRepository.Update(partner);
            await _unitOfWork.SaveChangesAsync(_);
            return Ok(_mapper.Map<PartnerDetailDto>(result));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}