namespace GAID.Domain.Models.Partner;

public class Partner : BaseEntity
{
    public Guid PartnerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Page.Page Page { get; set; } = new();

    public List<Program.Program> Programs { get; set; } = new();
    public List<Subscription.Subscription> Subscriptions { get; set; } = new();
}