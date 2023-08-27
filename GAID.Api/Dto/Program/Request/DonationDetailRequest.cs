using GAID.Domain.Models.Donation;

namespace GAID.Api.Dto.Program.Request;

public class DonationDetailRequest
{
    public string Reason { get; set; } = string.Empty;
    public string Remark { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public PaymentMethod Method { get; set; }
}