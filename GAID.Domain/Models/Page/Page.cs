namespace GAID.Domain.Models.Page;

public class Page : BaseEntity
{
    public Guid PageId { get; set; }
    public PageType PageType { get; set; }
    public string Content { get; set; } = string.Empty;
    public Guid? ProgramId { get; set; }
    public Guid? PartnerId { get; set; }
}