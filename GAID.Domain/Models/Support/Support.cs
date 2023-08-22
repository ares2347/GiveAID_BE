using System.ComponentModel.DataAnnotations.Schema;

namespace GAID.Domain.Models.Support;

public class Support : BaseEntity
{
    public Guid SupportId { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Reply { get; set; } = string.Empty;
    public SupportStatus Status { get; set; }
    
    [ForeignKey("Program")]
    public Guid ProgramId { get; set; }
    public Program.Program Program { get; set; } = new();
    
    [ForeignKey("Partner")]
    public Guid PartnerId { get; set; }
    public Partner.Partner Partner { get; set; } = new();
    
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    public User.User User { get; set; } = new();
}