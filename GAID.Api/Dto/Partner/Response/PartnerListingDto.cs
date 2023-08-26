using GAID.Api.Dto.Attachment;

namespace GAID.Api.Dto.Partner.Response;

public class PartnerListingDto
{
    public Guid PartnerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public AttachmentDetailDto PartnerThumbnail { get; set; } = new();
}