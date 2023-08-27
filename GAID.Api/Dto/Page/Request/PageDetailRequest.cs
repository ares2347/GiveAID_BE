using GAID.Domain.Models.Page;

namespace GAID.Api.Dto.Page.Request;

public class PageDetailRequest
{
    public PageType PageType { get; set; }
    public string Content { get; set; } = string.Empty;
    public Guid? ProgramId { get; set; }
    public Guid? PartnerId { get; set; }
}