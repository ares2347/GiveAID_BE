using AutoMapper;
using GAID.Api.Dto.Partner.Request;
using GAID.Api.Dto.Partner.Response;
using GAID.Api.Dto.Program.Request;
using GAID.Api.Dto.Program.Response;
using GAID.Application.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GAID.Api.Controllers.Program;

[Route("api/[controller]")]
[ApiController]
public class ProgramController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProgramController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    [HttpGet]
    public ActionResult<IQueryable<ProgramListingDto>> GetPrograms(int page = 0, int size = 10, CancellationToken _ = default)
    {
        var partners = _unitOfWork.ProgramRepository.Get(x => !x.IsDelete, size, page)
            .Select(x => _mapper.Map<ProgramListingDto>(x));
        return Ok(partners);
    }

    [HttpGet("{programId:guid}")]
    public async Task<ActionResult<ProgramDetailDto>> GetProgramById([FromRoute] Guid programId, CancellationToken _ = default)
    {
        try
        {
            var program = await _unitOfWork.ProgramRepository.GetById(programId, _);
            if (program is null) return NotFound();
            var result = _mapper.Map<ProgramDetailDto>(program);
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
    public async Task<ActionResult<PartnerDetailDto>> CreateProgram(ProgramDetailRequest request, CancellationToken _ = default)
    {
        try
        {
            var entity = _mapper.Map<Domain.Models.Program.Program>(request);
            entity.ProgramThumbnailId = request.ProgramThumbnailId;
            var result = _unitOfWork.ProgramRepository.Create(entity);
            await _unitOfWork.SaveChangesAsync(_);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    //TODO: Add role to authorization
    [Authorize]
    [HttpPut("update/{programId:guid}")]
    public async Task<ActionResult<PartnerDetailDto>> UpdateProgram([FromRoute] Guid programId, ProgramDetailRequest request, CancellationToken _ = default)
    {
        try
        {
            var program = await _unitOfWork.ProgramRepository.GetById(programId, _);
            if (program is null) return NotFound();
            program.ProgramThumbnailId = request.ProgramThumbnailId;
            program.Name = request.Name;
            program.Description = request.Description;
            program.DonationInfo = request.DonationInfo;
            program.DonationReason = request.DonationReason;
            program.Target = request.Target;
            program.EndDate = request.EndDate;
            // program.Page = request.Page;
            var result = _unitOfWork.ProgramRepository.Update(program);
            await _unitOfWork.SaveChangesAsync(_);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    
}