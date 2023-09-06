using GAID.Application.Email;
using GAID.Application.Repositories.Attachment;
using GAID.Application.Repositories.Donation;
using GAID.Application.Repositories.Page;
using GAID.Application.Repositories.Partner;
using GAID.Application.Repositories.Program;
using GAID.Domain;
using GAID.Shared;
using Microsoft.AspNetCore.Identity;

namespace GAID.Application.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(AppDbContext dbContext, UserManager<Domain.Models.User.User> userManager, UserContext userContext, IEmailService emailService)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _userContext = userContext;
        _emailService = emailService;
    }

    private readonly AppDbContext _dbContext;
    private readonly UserManager<Domain.Models.User.User> _userManager;
    private readonly UserContext _userContext;
    private readonly IEmailService _emailService;
    private AttachmentRepository? _attachmentRepository;
    private PartnerRepository? _partnerRepository;
    private ProgramRepository? _programRepository;
    private PageRepository? _pageRepository;
    private DonationRepository? _donationRepository;

    public AttachmentRepository AttachmentRepository
    {
        get
        {
            if (_attachmentRepository is null)
            {
                _attachmentRepository = new AttachmentRepository(_dbContext, _userContext, _userManager);
            }

            return _attachmentRepository;
        }
    }

    public PartnerRepository PartnerRepository =>
        _partnerRepository ??= new PartnerRepository(_dbContext, _userContext, _userManager);

    public ProgramRepository ProgramRepository =>
        _programRepository ??= new ProgramRepository(_dbContext, _userContext, _userManager, _emailService);

    public PageRepository PageRepository =>
        _pageRepository ??= new PageRepository(_dbContext, _userContext, _userManager);    
    public DonationRepository DonationRepository =>
        _donationRepository ??= new DonationRepository(_dbContext, _userContext, _userManager);


    public async Task<bool> SaveChangesAsync(CancellationToken _ = default)
    {
        return await _dbContext.SaveChangesAsync(_) > 0;
    }
}