using GAID.Api.Dto.Attachment;
using GAID.Api.Dto.Enrollment.Response;
using GAID.Api.Dto.Page.Response;

namespace GAID.Api.Dto.Program.Response;

public class ProgramDetailDto
{
    public Guid ProgramId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string DonationInfo { get; set; } = string.Empty;
    public List<string> DonationReason { get; set; } =new();
    public decimal Target { get; set; }
    public DateOnly EndDate { get; set; }
    public DateOnly StartDate { get; set; }
    public PageDetailDto Page { get; set; } = new();
    public decimal TotalDonation { get; set; }
    public bool IsClosed { get; set; } = false;
    public string ClosedReason { get; set; } = string.Empty;
    public AttachmentDetailDto ProgramThumbnail { get; set; } = new();
    public List<EnrollmentListingDto> Enrollments { get; set; } = new();
    public EnrollmentListingDto CurrentUserEnrollment { get; set; } = new();
    public Guid PartnerId { get; set; }
    public string PartnerName { get; set; } = string.Empty;
    public DateTimeOffset? ModifiedAt { get; set; } = DateTimeOffset.UtcNow;
    public Guid? ModifiedById { get; set; }
    public string? ModifiedByName { get; set; }
    public DateTimeOffset? CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public Guid? CreatedById { get; set; }
    public string? CreatedByName { get; set; }
}