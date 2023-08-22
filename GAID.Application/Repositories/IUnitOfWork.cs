namespace GAID.Application.Repositories;

public interface IUnitOfWork
{
    IBaseRepository<Domain.Models.Attachment.Attachment> AttachmentRepository { get; }
    Task<bool> SaveChangesAsync(CancellationToken _ = default);
}
