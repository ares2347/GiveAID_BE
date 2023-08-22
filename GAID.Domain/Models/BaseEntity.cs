namespace GAID.Domain.Models;

public class BaseEntity
{
    public bool IsDelete { get; set; } = false;
    public DateTimeOffset? ModifiedAt { get; set; } = DateTimeOffset.UtcNow;
    public User.User? ModifiedBy { get; set; } 
    public DateTimeOffset? CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public User.User? CreatedBy { get; set; }
}