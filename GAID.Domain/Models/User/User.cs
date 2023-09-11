using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace GAID.Domain.Models.User;

public class User : IdentityUser<Guid>
{
    public string? FullName { get; set; }
    public Guid? ProfilePictureId { get; set; }
    public DateOnly DateOfBirth { get; set; }

    public string PaymentInformation { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    [NotMapped] public List<string> Roles { get; set; } = new();
}