using GAID.Application.Repositories.Attachment;
using GAID.Application.Repositories.Page;
using GAID.Application.Repositories.Partner;
using GAID.Application.Repositories.Program;

namespace GAID.Application.Repositories;

public interface IUnitOfWork
{
    AttachmentRepository AttachmentRepository { get; }
    PartnerRepository PartnerRepository { get; }
    ProgramRepository ProgramRepository { get; }
    PageRepository PageRepository { get; }
    Task<bool> SaveChangesAsync(CancellationToken _ = default);
}
