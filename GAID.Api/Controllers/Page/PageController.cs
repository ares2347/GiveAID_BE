using System.Web;
using AutoMapper;
using GAID.Api.Dto.Page.Request;
using GAID.Api.Dto.Page.Response;
using GAID.Application.Repositories;
using GAID.Domain.Models.Page;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GAID.Api.Controllers.Page;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PageController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public PageController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [HttpGet("page/{page}")]
    public async Task<ActionResult<PageDetailDto>> GetPage(PageType type, CancellationToken _ = default)
    {
        try
        {
            var page = await _unitOfWork.PageRepository.GetByType(type, null, null, _);
            if (page is null) return NotFound();
            return Ok(_mapper.Map<PageDetailDto>(page));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("create")]
    public async Task<ActionResult<PageDetailDto>> CreatePage(PageDetailRequest request, CancellationToken _ = default)
    {
        try
        {
            var existedPage =
                await _unitOfWork.PageRepository.GetByType(request.PageType, request.PartnerId, request.ProgramId, _);
            if (existedPage is not null) return BadRequest("Page map to this entity has already existed. Use update page instead.");
            var entity = _mapper.Map<Domain.Models.Page.Page>(request);
            entity.Content = HttpUtility.HtmlEncode(request.Content);
            var result = await _unitOfWork.PageRepository.Create(entity);
            await _unitOfWork.SaveChangesAsync(_);
            return Ok(_mapper.Map<PageDetailDto>(result));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("update/{pageId:guid}")]
    public async Task<ActionResult<PageDetailDto>> UpdatePage([FromRoute] Guid pageId, PageDetailRequest request,
        CancellationToken _ = default)
    {
        try
        {
            var page = await _unitOfWork.PageRepository.GetById(pageId, _);
            if (page is null) return NotFound();
            page.PageType = request.PageType;
            page.Content = request.Content;
            page.ProgramId = request.ProgramId;
            page.PartnerId = request.PartnerId;
            var result = await _unitOfWork.PageRepository.Update(page);
            await _unitOfWork.SaveChangesAsync(_);
            return Ok(_mapper.Map<PageDetailDto>(result));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}