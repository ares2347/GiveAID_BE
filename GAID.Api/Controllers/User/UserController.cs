using System.Net;
using AutoMapper;
using GAID.Api.Dto.User.Request;
using GAID.Api.Dto.User.Response;
using GAID.Application.Attachment;
using GAID.Application.Repositories;
using GAID.Shared;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WDA.Service.User;
using IAuthorizationService = GAID.Application.Authorization.IAuthorizationService;


namespace GAID.Api.Controllers.User;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly UserContext _userContext;
    private readonly UserManager<Domain.Models.User.User> _userManager;
    private readonly IAuthorizationService _authorizationService;
    private readonly IMapper _mapper;
    private readonly IAttachmentService _attachmentService;
    private readonly IUnitOfWork _unitOfWork;

    public UserController(
        UserContext userContext,
        UserManager<Domain.Models.User.User> userManager,
        IAuthorizationService authorizationService,
        IMapper mapper,
        IAttachmentService attachmentService,
        IUnitOfWork unitOfWork)
    {
        _userContext = userContext;
        _userManager = userManager;
        _authorizationService = authorizationService;
        _mapper = mapper;
        _attachmentService = attachmentService;
        _unitOfWork = unitOfWork;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<TokenResult?>> Login(LoginRequest request, CancellationToken _)
    {
        if (string.IsNullOrWhiteSpace(request.Identifier))
        {
            return ValidationProblem($"Invalid {nameof(request.Identifier)}");
        }

        if (string.IsNullOrWhiteSpace(request.Password))
        {
            return ValidationProblem($"Invalid {nameof(request.Password)}");
        }

        try
        {
            var jwt = await _authorizationService.AuthorizeUser(request.Identifier, request.Password, _);
            return Ok(jwt);
        }
        catch (Exception e)
        {
            return Unauthorized(e.Message);
        }
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<TokenResult?>> Register(RegisterRequest request, CancellationToken _)
    {
        if (!Helper.ValidateEmailString(request.Email))
        {
            return ValidationProblem($"Invalid {nameof(request.Email)}");
        }

        if (string.IsNullOrWhiteSpace(request.Password))
        {
            return ValidationProblem($"Invalid {nameof(request.Password)}");
        }

        if (string.IsNullOrEmpty(request.FullName))
        {
            return ValidationProblem($"Invalid {nameof(request.FullName)}");
        }

        var user = new Domain.Models.User.User()
        {
            UserName = request.Username ?? request.Email,
            FullName = request.FullName,
            PhoneNumber = request.Phone,
            Email = request.Email,
            DateOfBirth = request.DateOfBirth,
            CreatedAt = DateTimeOffset.UtcNow
        };
        try
        {
            var res = await _authorizationService.RegisterUser(user, request.Roles, request.Password, _);
            if (res is null) return NotFound("User not found.");
            var token = await _authorizationService.IssueToken(res, _);
            return Ok(token);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }

    [HttpPost("change-password")]
    public async Task<ActionResult<bool?>> ChangePassword(ChangePasswordRequest request, CancellationToken _)
    {
        if (string.IsNullOrWhiteSpace(request.OldPassword))
        {
            return ValidationProblem($"Invalid {nameof(request.OldPassword)}");
        }

        if (string.IsNullOrWhiteSpace(request.NewPassword))
        {
            return ValidationProblem($"Invalid {nameof(request.NewPassword)}");
        }

        try
        {
            var res = await _authorizationService.ChangePassword(request.UserId, request.OldPassword,
                request.NewPassword, _);
            return Ok(res.Succeeded);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("me")]
    public async Task<ActionResult<UserInfoResponse>> GetUserInfo(CancellationToken _)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(_userContext.UserId.ToString());
            if (user is null) return NotFound();
            user.Roles = (await _userManager.GetRolesAsync(user)).ToList();
            var res = _mapper.Map<UserInfoResponse>(user);
            return Ok(res);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost("ProfilePicture")]
    [RequestSizeLimit(5 * 1014 * 1024)]
    public async Task<IdentityResult?> UploadProfilePicture(IFormFile file,
        CancellationToken _)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(_userContext.UserId.ToString());
            var attachmentId = NewId.NextGuid();
            var filePath =
                await _attachmentService.SaveFileAsync(file.OpenReadStream(), attachmentId.ToString(), file.ContentType);

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

            var res = await _unitOfWork.AttachmentRepository.Create(attachment);
            await _unitOfWork.SaveChangesAsync(_);
            user!.ProfilePictureId = res?.AttachmentId;
            return await _userManager.UpdateAsync(user);
        }
        catch (Exception e)
        {
            throw new HttpException("Upload profile picture failed.", HttpStatusCode.BadRequest);
        }
    }

    [AllowAnonymous]
    [HttpGet("token-validation")]
    public TokenResult ValidateToken(string validationToken)
    {
        return _authorizationService.ValidateToken(validationToken);
    }
}