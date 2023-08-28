using Microsoft.AspNetCore.Identity;

namespace GAID.Domain.Models.User;

public class Role : IdentityRole<Guid>
{

}

public static class RoleName
{
    public const string Admin = "Admin";
    public const string User = "User";
    public const string Partner = "Partner";
}