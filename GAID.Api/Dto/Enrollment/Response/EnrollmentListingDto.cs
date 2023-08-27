namespace GAID.Api.Dto.Enrollment.Response;

public class EnrollmentListingDto
{
    public Guid EnrollmentId { get; set; }
    public Guid ProgramId { get; set; }
    public DateTimeOffset? CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public Guid? CreatedById { get; set; }
    public string? CreatedByName { get; set; }
}