using GAID.Domain.Models.Page;

namespace GAID.Api.Dto.Page.Response;

public class PageDetailDto
{
    public Guid PageId { get; set; }
    public PageType PageType { get; set; }
    public string Content { get; set; } = string.Empty;
    public Guid? ProgramId { get; set; }
    public Guid? PartnerId { get; set; }
    public DateTimeOffset? ModifiedAt { get; set; } = DateTimeOffset.UtcNow;
    public Guid? ModifiedById { get; set; }
    public string? ModifiedByName { get; set; }
    public DateTimeOffset? CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public Guid? CreatedById { get; set; }
    public string? CreatedByName { get; set; }
}