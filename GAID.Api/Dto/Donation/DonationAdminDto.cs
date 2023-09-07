using GAID.Domain.Models.Donation;

namespace GAID.Api.Dto.Donation;

public class DonationAdminDto
{
    public Guid DonationId { get; set; }
    public string Reason { get; set; } = string.Empty;
    public DonationStatus Status { get; set; }
    public string Remark { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public PaymentMethod Method { get; set; } = PaymentMethod.Paypal;
    public string PaypalOrderId { get; set; } = string.Empty;
    public Guid PartnerId { get; set; }
    public Guid ProgramId { get; set; }
    public Guid EnrollmentId { get; set; }
    public string PartnerName { get; set; } = string.Empty;
    public string ProgramName { get; set; } = string.Empty;
    public string CreatedByName { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
}