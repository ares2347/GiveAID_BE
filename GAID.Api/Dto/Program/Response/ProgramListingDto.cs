using GAID.Api.Dto.Partner.Response;

namespace GAID.Api.Dto.Program.Response;

public class ProgramListingDto
{
    public Guid ProgramId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Target { get; set; }
    public DateOnly EndDate { get; set; }
    public Guid PartnerId { get; set; }
    public PartnerListingDto? Partner { get; set; }
    public Domain.Models.Attachment.Attachment ProgramThumbnail { get; set; } = new();
}