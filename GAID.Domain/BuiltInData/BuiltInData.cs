using GAID.Domain.Models.Email;
using GAID.Domain.Models.User;
using Microsoft.AspNetCore.Identity;

namespace GAID.Domain.BuiltInData;

public static class BuiltInData
{
    private const string UserFullName = "System Admin";
    private const string UserEmail = "admin@email.com";
    private const string UserIdentifier = "admin";

    public static List<User> SeedUserData() => new List<User>
    {
        new User
        {
            Id = Guid.Parse("10000000-0000-0000-0000-000000000001"),
            UserName = UserIdentifier,
            FullName = UserFullName,
            NormalizedUserName = UserIdentifier.Normalize(),
            Email = UserEmail,
            EmailConfirmed = true,
            NormalizedEmail = UserEmail.Normalize(),
            PasswordHash = "AQAAAAIAAYagAAAAEMeRmOWs9W/KsBTc0NEYwk5Efsp1rjs48fPIPWSW0xhuuKWByjTRlnJXKrEmn9yPhA==",
            SecurityStamp = "3YYPM246ONSVZFAKY3TR2JSVKMX7ZM4D",
            ConcurrencyStamp = "6a9943f8-af5b-4231-9a8d-63f8c43c6e0c",
        }
    };

    public static List<Role> SeedRoleData() => new List<Role>
    {
        new Role
        {
            Name = RoleName.Admin,
            NormalizedName = RoleName.Admin.Normalize(),
            Id = Guid.Parse("20000000-0000-0000-0000-000000000001")
        },
        new Role
        {
            Name = RoleName.Partner,
            NormalizedName = RoleName.Partner.Normalize(),
            Id = Guid.Parse("20000000-0000-0000-0000-000000000002")
        },
        new Role
        {
            Name = RoleName.User,
            NormalizedName = RoleName.User.Normalize(),
            Id = Guid.Parse("20000000-0000-0000-0000-000000000003")
        }
    };

    public static List<EmailTemplate> SeedEmailTemplates() => new()
    {
        new EmailTemplate
        {
            EmailTemplateId = Guid.Parse("30000000-0000-0000-0000-000000000001"),
            EmailTemplateType = EmailTemplateType.DonationUserTemplate,
            Subject = "[Give-AID] Your Generosity is Changing Lives - Thank You for Your Donation!",
            Body = GetText($"{AppContext.BaseDirectory}/Models/Email/BuiltInTemplates/DonationUserTemplate.txt")
        },
        new EmailTemplate
        {
            EmailTemplateId = Guid.Parse("30000000-0000-0000-0000-000000000002"),
            EmailTemplateType = EmailTemplateType.EnrollmentUserTemplate,
            Subject = "[Give-AID] Thank You for Enrolling in Our NGO Program!",
            Body = GetText($"{AppContext.BaseDirectory}/Models/Email/BuiltInTemplates/EnrollmentUserTemplate.txt")
        },
        new EmailTemplate
        {
            EmailTemplateId = Guid.Parse("30000000-0000-0000-0000-000000000003"),
            EmailTemplateType = EmailTemplateType.ProgramCloseTemplate,
            Subject = "[Give-AID] Important Update: Closure of Our Charity Program",
            Body = GetText($"{AppContext.BaseDirectory}/Models/Email/BuiltInTemplates/ProgramCloseTemplate.txt")
        },
        new EmailTemplate
        {
            EmailTemplateId = Guid.Parse("30000000-0000-0000-0000-000000000004"),
            EmailTemplateType = EmailTemplateType.ShareProgram,
            Subject = "[Give-AID] Join Us in Making a Difference: Invitation to Our NGO Program",
            Body = GetText($"{AppContext.BaseDirectory}/Models/Email/BuiltInTemplates/ShareProgram.txt")
        }
    };

    public static List<IdentityUserRole<Guid>> SeedUserRoles() => new List<IdentityUserRole<Guid>>
    {
        new IdentityUserRole<Guid>
        {
            RoleId = Guid.Parse("20000000-0000-0000-0000-000000000001"),
            UserId = Guid.Parse("10000000-0000-0000-0000-000000000001")
        }
    };

    #region private method

    private static string GetText(string filePath)
    {
        return File.ReadAllText(filePath);
    }

    #endregion
}