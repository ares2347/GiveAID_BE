using AutoMapper;
using GAID.Api.Dto;
using GAID.Api.Dto.Program;
using GAID.Api.Dto.Program.Request;
using GAID.Api.Dto.Program.Response;
using GAID.Application.Email;
using GAID.Application.Repositories;
using GAID.Domain.Models.Donation;
using GAID.Domain.Models.Email;
using GAID.Shared;
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
    private readonly IEmailService _emailService;
    private readonly UserContext _userContext;

    public ProgramController(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService,
        UserContext userContext)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _emailService = emailService;
        _userContext = userContext;
    }

    [HttpGet]
    public ActionResult<ListingResult<ProgramListingDto>> GetPrograms(string? search, bool isActive = true,
        int page = 0, int size = 10,
        CancellationToken _ = default)
    {
        var query = _unitOfWork.ProgramRepository.Get(x => !x.IsDelete && (isActive && !x.IsClosed && x.IsOpen), size,
            page);
        var total = _unitOfWork.ProgramRepository.Count(x => !x.IsDelete && (isActive && !x.IsClosed && x.IsOpen));
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(x => x.Name.Contains(search) || x.Partner.Name.Contains(search));
            total = _unitOfWork.ProgramRepository.Count(x =>
                !x.IsDelete && (x.Name.Contains(search) || x.Partner.Name.Contains(search)) &&
                (isActive && !x.IsClosed && x.IsOpen));
        }

        var partners = query
            .Select(x => _mapper.Map<ProgramListingDto>(x));

        return Ok(new ListingResult<ProgramListingDto>
        {
            Data = partners,
            Page = page,
            Size = size,
            Total = total
        });
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
            if (request.StartDate < DateOnly.FromDateTime(DateTime.Today))
                return BadRequest("Start Date invalid.");
            var entity = _mapper.Map<Domain.Models.Program.Program>(request);
            var attachment = await _unitOfWork.AttachmentRepository.GetById(request.ProgramThumbnailId, _);
            if (attachment is not null) entity.ProgramThumbnail = attachment;
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
            if (attachment is not null) program.ProgramThumbnail = attachment;
            program.Name = request.Name;
            program.Description = request.Description;
            program.DonationInfo = request.DonationInfo;
            program.DonationReason = JsonConvert.SerializeObject(request.DonationReason);
            program.Target = request.Target;
            program.EndDate = request.EndDate;
            if (program.StartDate > DateOnly.FromDateTime(DateTime.Today))
                program.StartDate = request.StartDate;
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
    public async Task<ActionResult<ProgramDetailDto>> EnrollProgram([FromRoute] Guid programId,
        CancellationToken _ = default)
    {
        try
        {
            var result = await _unitOfWork.ProgramRepository.AddEnrollment(programId, _);
            await _unitOfWork.SaveChangesAsync(_);
            var subjectReplacements = new Dictionary<string, string> { };
            var bodyReplacements = new Dictionary<string, string>
            {
                { "Recipient_Name", $"{_userContext.FullName} " },
                { "Program_Name", $"{result?.Name}" },
                { "Partner_Name", $"{result?.Partner?.Name}" },
                { "Program_Url", $"{AppSettings.Instance.ClientConfiguration.SiteBaseUrl}/Program/{programId}" },
                { "Home_Url", $"{AppSettings.Instance.ClientConfiguration.SiteBaseUrl}" }
            };
            if (_userContext.Email is not null)
                await _emailService.SendEmailNotification(EmailTemplateType.EnrollmentUserTemplate, _userContext.Email,
                    subjectReplacements, bodyReplacements, _: _);

            //end of send email
            return Ok(_mapper.Map<ProgramDetailDto>(result));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpPut("close/{programId:guid}")]
    public async Task<ActionResult<ProgramDetailDto>> CloseProgram([FromRoute] Guid programId,
        CloseProgramRequest request, CancellationToken _ = default)
    {
        //TODO: Trigger email notifications
        try
        {
            var result = await _unitOfWork.ProgramRepository.GetById(programId, _);
            if (result is null) return NotFound("Program not found");
            if (result.IsClosed) return BadRequest("Program has already been closed.");
            result.IsClosed = true;
            result.ClosedReason = request.CloseReason;
            await _unitOfWork.SaveChangesAsync(_);
            var subjectReplacements = new Dictionary<string, string> { };
            var bodyReplacements = new Dictionary<string, string>
            {
                { "Recipient_Name", $"{_userContext.FullName} " },
                { "Program", $"{result?.Name}" },
                { "Program_Name", $"{result?.Name}" },
                { "Partner", $"{result?.Partner?.Name}" },
                { "Partner_Name", $"{result?.Partner?.Name}" },
                { "Donation_Amount", $"{result?.TotalDonation}" },
                {
                    "Donation_Duration",
                    $"{result?.EndDate.DayNumber - result?.StartDate.DayNumber}"
                },
                { "Donation_End_Date", $"{result?.EndDate}" },
                { "Program_Url", $"{AppSettings.Instance.ClientConfiguration.SiteBaseUrl}/Program/{programId}" },
                { "[Program_Url]", $"{AppSettings.Instance.ClientConfiguration.SiteBaseUrl}/Program/{programId}" },
                { "Home_Url", $"{AppSettings.Instance.ClientConfiguration.SiteBaseUrl}" }
            };
            if (_userContext.Email is not null)
                await _emailService.SendEmailNotification(EmailTemplateType.ProgramCloseTemplate, _userContext.Email,
                    subjectReplacements, bodyReplacements, _: _);

            //end of send email
            return Ok(_mapper.Map<ProgramDetailDto>(result));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpPost("share/{programId:guid}")]
    public async Task<ActionResult> ShareProgram([FromRoute] Guid programId,
        ShareProgramRequest request, CancellationToken _ = default)
    {
        try
        {
            var result = await _unitOfWork.ProgramRepository.GetById(programId, _);
            if (result is null) return NotFound("Program not found");
            if (result.IsClosed) return BadRequest("Program has already been closed.");
            foreach (var email in request.Email)
            {
                var subjectReplacements = new Dictionary<string, string> { };
                var bodyReplacements = new Dictionary<string, string>
                {
                    { "Recipient_Name", $"{email} " },
                    { "Program_Name", $"{result?.Name}" },
                    { "Partner_Name", $"{result?.Partner?.Name}" },
                    { "Start_Date", $"{result?.StartDate}" },
                    { "End_Date", $"{result?.EndDate}" },
                    { "Target", $"{result?.Target}" },
                    { "Program_Url", $"{AppSettings.Instance.ClientConfiguration.SiteBaseUrl}/Program/{programId}" },
                    { "[Program_Url]", $"{AppSettings.Instance.ClientConfiguration.SiteBaseUrl}/Program/{programId}" },
                    { "Home_Url", $"{AppSettings.Instance.ClientConfiguration.SiteBaseUrl}" }
                };
                if (Helper.ValidateEmailString(email))
                    await _emailService.SendEmailNotification(EmailTemplateType.ShareProgram, email,
                        subjectReplacements, bodyReplacements, _: _);
            }

            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}