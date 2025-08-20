using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Persistence.Entities
{
    [Table("Organization", Schema = "ai")]
    public class Organization
    {
        [Key]
        [Column("organization_id")]
        public Guid OrganizationId { get; set; } = Guid.NewGuid();

        [Column("organization_name")]
        [Required]
        [MaxLength(255)]
        public string OrganizationName { get; set; } = string.Empty;

        [Column("status")]
        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Active";

        [Column("billing_plan")]
        [MaxLength(50)]
        public string? BillingPlan { get; set; }

        [Column("contact_email")]
        [MaxLength(255)]
        [EmailAddress]
        public string? ContactEmail { get; set; }

        [Column("created_at", TypeName = "timestamp with time zone")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at", TypeName = "timestamp with time zone")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<Project>? Projects { get; set; }
        public ICollection<Milestone>? Milestones { get; set; }
        public ICollection<OrganizationUser>? OrganizationUsers { get; set; }
        public ICollection<Task>? Tasks { get; set; }
        public ICollection<Issue>? Issues { get; set; }
        public ICollection<Risk>? Risks { get; set; }
        public ICollection<Resource>? Resources { get; set; }
        public ICollection<ChangeRequest>? ChangeRequests { get; set; }
        public ICollection<TaskTimeLog>? TaskTimeLogs { get; set; }
    }
} 