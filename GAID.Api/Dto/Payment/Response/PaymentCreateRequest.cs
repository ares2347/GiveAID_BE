using GAID.Domain.Models.Donation;

namespace GAID.Api.Dto.Payment.Response;

public class PaymentCreateRequest
{
    public string Reason { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public Guid ProgramId { get; set; }
}