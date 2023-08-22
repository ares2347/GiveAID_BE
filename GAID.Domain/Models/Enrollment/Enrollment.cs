using System.ComponentModel.DataAnnotations.Schema;

namespace GAID.Domain.Models.Enrollment;

public class Enrollment : BaseEntity
{
    public Guid EnrollmentId { get; set; }
    
    [ForeignKey("Program")]
    public Guid ProgramId { get; set; }
    public Program.Program Program { get; set; } = new();
    
    public List<Donation.Donation> Donations { get; set; } = new();
}