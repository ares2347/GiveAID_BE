namespace GAID.Api.Dto.Program.Request;

public class ProgramDetailRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string DonationInfo { get; set; } = string.Empty;
    public List<string> DonationReason { get; set; } = new();
    public decimal Target { get; set; }
    public DateOnly EndDate { get; set; }
    public DateOnly StartDate { get; set; }
    public Guid ProgramThumbnailId { get; set; } = new();
    // public Page Page { get; set; } = new();
    public Guid PartnerId { get; set; }
}