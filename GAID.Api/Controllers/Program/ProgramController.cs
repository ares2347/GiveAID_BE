using AutoMapper;
using GAID.Api.Dto.Program.Request;
using GAID.Api.Dto.Program.Response;
using GAID.Application.Repositories;
using GAID.Domain.Models.Donation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
    public ActionResult<IQueryable<ProgramListingDto>> GetPrograms(int page = 0, int size = 10,
        CancellationToken _ = default)
    {
        var partners = _unitOfWork.ProgramRepository.Get(x => !x.IsDelete, size, page)
            .Select(x => _mapper.Map<ProgramListingDto>(x));
        return Ok(partners);
    }

    [HttpGet("{programId:guid}")]
    public async Task<ActionResult<ProgramDetailDto>> GetProgramById([FromRoute] Guid programId,
        CancellationToken _ = default)
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
    public async Task<ActionResult<ProgramDetailDto>> CreateProgram(ProgramDetailRequest request,
        CancellationToken _ = default)
    {
        try
        {
            var entity = _mapper.Map<Domain.Models.Program.Program>(request);
            var attachment = await _unitOfWork.AttachmentRepository.GetById(request.ProgramThumbnailId, _);
            if(attachment is not null) entity.ProgramThumbnail = attachment;
            entity.DonationReason = JsonConvert.SerializeObject(request.DonationReason);
            var result = await _unitOfWork.ProgramRepository.Create(entity);
            await _unitOfWork.SaveChangesAsync(_);
            return Ok(_mapper.Map<ProgramDetailDto>(result));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    //TODO: Add role to authorization
    [Authorize]
    [HttpPut("update/{programId:guid}")]
    public async Task<ActionResult<ProgramDetailDto>> UpdateProgram([FromRoute] Guid programId,
        ProgramDetailRequest request, CancellationToken _ = default)
    {
        try
        {
            var program = await _unitOfWork.ProgramRepository.GetById(programId, _);
            if (program is null) return NotFound();
            var attachment = await _unitOfWork.AttachmentRepository.GetById(request.ProgramThumbnailId, _);
            if(attachment is not null) program.ProgramThumbnail = attachment;
            program.Name = request.Name;
            program.Description = request.Description;
            program.DonationInfo = request.DonationInfo;
            program.DonationReason = JsonConvert.SerializeObject(request.DonationReason);
            program.Target = request.Target;
            program.EndDate = request.EndDate;
            // program.Page = request.Page;
            var result = await _unitOfWork.ProgramRepository.Update(program);
            await _unitOfWork.SaveChangesAsync(_);
            return Ok(_mapper.Map<ProgramDetailDto>(result));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPut("enroll/{programId:guid}")]
    public async Task<ActionResult<ProgramDetailDto>> EnrollProgram([FromRoute] Guid programId, CancellationToken _ = default)
    {
        try
        {
            var result = await _unitOfWork.ProgramRepository.AddEnrollment(programId, _);
            await _unitOfWork.SaveChangesAsync(_);
            return Ok(_mapper.Map<ProgramDetailDto>(result));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    // [HttpPut("donate/{programId:guid}")]
    // public async Task<ActionResult<ProgramDetailDto>> DonateProgram([FromRoute] Guid programId, DonationDetailRequest request, CancellationToken _ = default)
    // {
    //     try
    //     {
    //         var donation = _mapper.Map<Donation>(request);
    //         var result = await _unitOfWork.ProgramRepository.AddDonation(programId, donation, _);
    //         await _unitOfWork.SaveChangesAsync(_);
    //         return Ok(_mapper.Map<ProgramDetailDto>(result));
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.Message);
    //     }
    // }
}