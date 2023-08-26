using System.Net;
using AutoMapper;
using GAID.Api.Dto.Attachment;
using GAID.Application.Attachment;
using GAID.Application.Repositories;
using GAID.Shared;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GAID.Api.Controllers.Attachment;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AttachmentController : ControllerBase
{
    private readonly IAttachmentService _attachmentService;
    private readonly UserContext _userContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<Domain.Models.User.User> _userManager;
    private readonly IMapper _mapper;

    public AttachmentController(IAttachmentService attachmentService, UserContext userContext, IUnitOfWork unitOfWork,UserManager<Domain.Models.User.User> userManager, IMapper mapper)
    {
        _attachmentService = attachmentService;
        _userContext = userContext;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _mapper = mapper;
    }
    
    [HttpPost]
    [RequestSizeLimit(5 * 1014 * 1024)]
    public async Task<AttachmentDetailDto?> UploadAttachment(IFormFile file,
        CancellationToken _)
    {
        try
        {
            var attachmentId = NewId.NextGuid();
            var filePath =
                await _attachmentService.SaveFileAsync(file.OpenReadStream(), attachmentId.ToString());
            var user = await _userManager.FindByIdAsync(_userContext.UserId.ToString());
            var attachment = new Domain.Models.Attachment.Attachment
            {
                AttachmentId = attachmentId,
                Path = filePath,
                Name = file.FileName,
                Size = file.Length,
                ContentType = file.ContentType,
                CreatedBy = user,
                CreatedAt = DateTimeOffset.Now,
                ModifiedBy = user,
                ModifiedAt = DateTimeOffset.Now
            };

            var res = _unitOfWork.AttachmentRepository.Create(attachment);
            await _unitOfWork.SaveChangesAsync(_);
            return _mapper.Map<AttachmentDetailDto>(res);
        }
        catch (Exception e)
        {
            throw new HttpException("Upload profile picture failed.", HttpStatusCode.BadRequest);
        }
    }
    
}