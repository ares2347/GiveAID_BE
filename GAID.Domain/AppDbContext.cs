using GAID.Domain.Models.Attachment;
using GAID.Domain.Models.Donation;
using GAID.Domain.Models.Email;
using GAID.Domain.Models.Enrollment;
using GAID.Domain.Models.Page;
using GAID.Domain.Models.Partner;
using GAID.Domain.Models.Program;
using GAID.Domain.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GAID.Domain;

public class AppDbContext : IdentityDbContext<User, Role,  Guid>
{
    public DbSet<EmailTemplate> EmailTemplates { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<Partner> Partners { get; set; }
    public DbSet<Program> Programs { get; set; }
    public DbSet<Page> Pages { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Donation> Donations { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Program>()
            .HasOne(c => c.ProgramThumbnail)
            .WithOne()
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Partner>()
            .HasOne(c => c.PartnerThumbnail)
            .WithOne()
            .OnDelete(DeleteBehavior.NoAction);
        
        //Seed DB
        modelBuilder.Entity<User>()
            .HasData(BuiltInData.BuiltInData.SeedUserData());
        modelBuilder.Entity<Role>()
            .HasData(BuiltInData.BuiltInData.SeedRoleData());
        modelBuilder.Entity<IdentityUserRole<Guid>>()
            .HasData(BuiltInData.BuiltInData.SeedUserRoles());
        modelBuilder.Entity<EmailTemplate>().HasData(BuiltInData.BuiltInData.SeedEmailTemplates());
        
    }
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<DateOnly>()
            .HaveConversion<DateOnlyConverter>()
            .HaveColumnType("date");
    }
}

/// <summary>
///     Converts <see cref="DateOnly" /> to <see cref="DateTime" /> and vice versa.
/// </summary>
public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
{
    /// <summary>
    ///     Creates a new instance of this converter.
    /// </summary>
    public DateOnlyConverter() : base(
        d => d.ToDateTime(TimeOnly.MinValue),
        d => DateOnly.FromDateTime(d))
    {
    }
}