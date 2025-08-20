using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Persistence.Entities
{
    [Table("User", Schema = "ai")]
    public class User
    {
        [Key]
        [Column("user_id")]
        public Guid UserId { get; set; } = Guid.NewGuid();

        [Column("email")]
        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Column("password_hash")]
        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Column("first_name")]
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Column("last_name")]
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        [Column("created_at", TypeName = "timestamp with time zone")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at", TypeName = "timestamp with time zone")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<Project>? ManagedProjects { get; set; }
        public ICollection<OrganizationUser>? OrganizationUsers { get; set; }
        public ICollection<Task>? AssignedTasks { get; set; }
        public ICollection<Issue>? AssignedIssues { get; set; }
        public ICollection<Risk>? OwnedRisks { get; set; }
        public ICollection<Resource>? Resources { get; set; }
        public ICollection<ChangeRequest>? RequestedChangeRequests { get; set; }
        public ICollection<ChangeRequest>? ApprovedChangeRequests { get; set; }
        public ICollection<QualityAssurance>? ReportedQualityAssurances { get; set; }
        public ICollection<QualityAssurance>? AssignedQualityAssurances { get; set; }
    }
} 