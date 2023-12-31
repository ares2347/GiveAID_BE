namespace GAID.Api.Dto.Partner.Request;

public class PartnerDetailRequest
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid PartnerThumbnailId { get; set; }
    // public Domain.Models.Page.Page Page { get; set; } = new();
}