using GAID.Api.Dto.Enrollment.Response;
using GAID.Domain.Models.Page;

namespace GAID.Api.Dto.Program.Response;

public class ProgramDetailDto
{
    public Guid ProgramId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string DonationInfo { get; set; } = string.Empty;
    public string DonationReason { get; set; } = string.Empty;
    public decimal Target { get; set; }
    public DateOnly EndDate { get; set; }
    public Page Page { get; set; } = new();
    public Domain.Models.Attachment.Attachment ProgramThumbnail { get; set; } = new();
    public List<EnrollmentListingDto> Enrollments { get; set; } = new();
    public DateTimeOffset? ModifiedAt { get; set; } = DateTimeOffset.UtcNow;
    public Guid? ModifiedById { get; set; }
    public string? ModifiedByName { get; set; }
    public DateTimeOffset? CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public Guid? CreatedById { get; set; }
    public string? CreatedByName { get; set; }
}