using System.ComponentModel.DataAnnotations.Schema;

namespace GAID.Domain.Models.Subscription;

public class Subscription : BaseEntity
{
    public Guid SubscriptionId { get; set; }
    
    [ForeignKey("Partner")]
    public Guid PartnerId { get; set; }
    public Partner.Partner Partner { get; set; } = new();
}