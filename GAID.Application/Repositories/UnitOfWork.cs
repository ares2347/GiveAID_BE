using GAID.Application.Repositories.Attachment;
using GAID.Application.Repositories.Partner;
using GAID.Application.Repositories.Program;
using GAID.Domain;
using GAID.Domain.Models.User;
using Microsoft.AspNetCore.Identity;

namespace GAID.Application.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(AppDbContext dbContext, UserManager<Domain.Models.User.User> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    private readonly AppDbContext _dbContext;
    private readonly UserManager<Domain.Models.User.User> _userManager;
    private IBaseRepository<Domain.Models.Attachment.Attachment>? _attachmentRepository;
    private IBaseRepository<Domain.Models.Partner.Partner>? _partnerRepository;
    private IBaseRepository<Domain.Models.Program.Program>? _programRepository;

    public IBaseRepository<Domain.Models.Attachment.Attachment> AttachmentRepository
    {
        get
        {
            if (_attachmentRepository is null)
            {
                _attachmentRepository = new AttachmentRepository(_dbContext);
            }

            return _attachmentRepository;
        }
    }

    public IBaseRepository<Domain.Models.Partner.Partner> PartnerRepository => _partnerRepository ??= new PartnerRepository(_dbContext);

    public IBaseRepository<Domain.Models.Program.Program> ProgramRepository => _programRepository ??= new ProgramRepository(_dbContext);

    public async Task<bool> SaveChangesAsync(CancellationToken _ = default)
    {
        return await _dbContext.SaveChangesAsync(_) > 0;
    }
}