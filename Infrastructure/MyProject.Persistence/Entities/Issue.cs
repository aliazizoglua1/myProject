using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Persistence.Entities
{
    [Table("Issue", Schema = "ai")]
    public class Issue
    {
        [Key]
        [Column("issue_id")]
        public Guid IssueId { get; set; } = Guid.NewGuid();

        [Column("project_id")]
        [Required]
        public Guid ProjectId { get; set; }

        [Column("task_id")]
        public Guid? TaskId { get; set; }

        [Column("issue_name")]
        [Required]
        [MaxLength(255)]
        public string IssueName { get; set; } = string.Empty;

        [Column("issue_description")]
        [Required]
        public string IssueDescription { get; set; } = string.Empty;

        [Column("issue_type")]
        [MaxLength(100)]
        public string? IssueType { get; set; }

        [Column("root_cause")]
        public string? RootCause { get; set; }

        [Column("impact_on_schedule_days")]
        public int? ImpactOnScheduleDays { get; set; }

        [Column("impact_on_budget_usd", TypeName = "numeric(18,2)")] 
        [Range(0, double.MaxValue)]
        public decimal? ImpactOnBudgetUsd { get; set; }

        [Column("impact_on_scope")]
        public string? ImpactOnScope { get; set; }

        [Column("impact_on_quality")]
        public string? ImpactOnQuality { get; set; }

        [Column("resolution_steps")]
        public string? ResolutionSteps { get; set; }

        [Column("severity")]
        [Required]
        [MaxLength(50)]
        public string Severity { get; set; } = "Medium";

        [Column("priority")]
        [Required]
        [MaxLength(50)]
        public string Priority { get; set; } = "Normal";

        [Column("assigned_to_user_id")]
        public Guid? AssignedToUserId { get; set; }

        [Column("status")]
        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Open";

        [Column("opened_date")]
        [Required]
        public DateOnly OpenedDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        [Column("closed_date")]
        public DateOnly? ClosedDate { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [Column("tenant_id")]
        [Required]
        public Guid TenantId { get; set; }

        [Column("milestone_id")]
        public Guid? MilestoneId { get; set; }

        // Navigation properties
        public Project? Project { get; set; }
        public Task? Task { get; set; }
        public User? AssignedToUser { get; set; }
        public Organization? Tenant { get; set; }
        public Milestone? Milestone { get; set; }
    }
} 