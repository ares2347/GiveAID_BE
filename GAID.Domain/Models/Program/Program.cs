using System.ComponentModel.DataAnnotations.Schema;

namespace GAID.Domain.Models.Program;

public class Program : BaseEntity
{
    public Guid ProgramId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string DonationInfo { get; set; } = string.Empty;
    public string DonationReason { get; set; } = string.Empty;
    public decimal Target { get; set; }
    public DateOnly EndDate { get; set; }
    
    [ForeignKey("ProgramThumbnail")]
    public Guid ProgramThumbnailId { get; set; }
    public Attachment.Attachment ProgramThumbnail { get; set; } = new();
    public Page.Page Page { get; set; } = new();
    
    [ForeignKey("Partner")]
    public Guid PartnerId { get; set; }
    public Partner.Partner? Partner { get; set; }

    public List<Enrollment.Enrollment> Enrollments { get; set; } = new();

    [NotMapped] public decimal TotalDonation => Enrollments.Sum(x => x.Donations.Sum(y => y.Amount));
    [NotMapped] public Enrollment.Enrollment? CurrentUserEnrollment { get; set; }
}