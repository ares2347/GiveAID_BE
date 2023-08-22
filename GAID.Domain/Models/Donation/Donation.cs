using System.ComponentModel.DataAnnotations.Schema;

namespace GAID.Domain.Models.Donation;

public class Donation : BaseEntity
{
    public Guid DonationId { get; set; }
    public string Reason { get; set; } = string.Empty;
    public DonationStatus Status { get; set; }
    public string Remark { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public PaymentMethod Method { get; set; }
    
    [ForeignKey("Enrollment")]
    public Guid EnrollmentId { get; set; }
    public Enrollment.Enrollment Enrollment { get; set; } = new();
}