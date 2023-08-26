using GAID.Domain.Models.Page;

namespace GAID.Api.Dto.Program.Request;

public class ProgramDetailRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string DonationInfo { get; set; } = string.Empty;
    public string DonationReason { get; set; } = string.Empty;
    public decimal Target { get; set; }
    public DateOnly EndDate { get; set; }
    public Guid ProgramThumbnailId { get; set; } = new();
    // public Page Page { get; set; } = new();
    public Guid PartnerId { get; set; }
}