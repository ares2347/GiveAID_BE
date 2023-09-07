using GAID.Api.Dto.Donation;

namespace GAID.Api.Dto.Enrollment.Response;

public class EnrollmentListingDto
{
    public Guid EnrollmentId { get; set; }
    public Guid ProgramId { get; set; }
    public List<DonationDto> Donations { get; set; } = new();
    public DateTimeOffset? CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public Guid? CreatedById { get; set; }
    public string? CreatedByName { get; set; }
}