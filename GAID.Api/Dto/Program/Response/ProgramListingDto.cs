using GAID.Api.Dto.Attachment;
using GAID.Api.Dto.Partner.Response;

namespace GAID.Api.Dto.Program.Response;

public class ProgramListingDto
{
    public Guid ProgramId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Target { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateOnly EndDate { get; set; }
    public bool IsClosed { get; set; } = false;
    public string ClosedReason { get; set; } = string.Empty;
    public Guid PartnerId { get; set; }
    public PartnerListingDto? Partner { get; set; }
    public AttachmentDetailDto ProgramThumbnail { get; set; } = new();
}