namespace GAID.Application.Repositories;

public interface IUnitOfWork
{
    IBaseRepository<Domain.Models.Attachment.Attachment> AttachmentRepository { get; }
    IBaseRepository<Domain.Models.Partner.Partner> PartnerRepository { get; }
    IBaseRepository<Domain.Models.Program.Program> ProgramRepository { get; }
    Task<bool> SaveChangesAsync(CancellationToken _ = default);
}
