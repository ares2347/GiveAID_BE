using GAID.Api.Dto.Attachment;
using GAID.Api.Dto.Program.Response;
using GAID.Domain.Models.Page;

namespace GAID.Api.Dto.Partner.Response;

public class PartnerDetailDto
{
    public Guid PartnerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Page Page { get; set; } = new();
    public AttachmentDetailDto PartnerThumbnail { get; set; } = new();
    public List<ProgramListingDto> Programs { get; set; } = new();
    public DateTimeOffset? ModifiedAt { get; set; } = DateTimeOffset.UtcNow;
    public Guid? ModifiedById { get; set; }
    public string? ModifiedByName { get; set; }
    public DateTimeOffset? CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public Guid? CreatedById { get; set; }
    public string? CreatedByName { get; set; }
}