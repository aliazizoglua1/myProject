using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace MyProject.Persistence.Entities
{
    [Table("ChangeRequest", Schema = "ai")]
    public class ChangeRequest
    {
        [Key]
        [Column("change_request_id")]
        public Guid ChangeRequestId { get; set; } = Guid.NewGuid();

        [Column("project_id")]
        public Guid ProjectId { get; set; }

        [Column("request_title")]
        [Required]
        [MaxLength(255)]
        public string RequestTitle { get; set; } = string.Empty;

        [Column("request_description")]
        [Required]
        public string RequestDescription { get; set; } = string.Empty;

        [Column("reason_for_change")]
        [Required]
        public string ReasonForChange { get; set; } = string.Empty;

        [Column("estimated_impact_on_scope")]
        public string? EstimatedImpactOnScope { get; set; }

        [Column("estimated_impact_on_schedule_days")]
        public int? EstimatedImpactOnScheduleDays { get; set; }

        [Column("estimated_impact_on_budget_usd", TypeName = "numeric(18,2)")]
        [Range(0, double.MaxValue)]
        public decimal? EstimatedImpactOnBudgetUsd { get; set; }

        [Column("estimated_impact_on_quality")]
        public string? EstimatedImpactOnQuality { get; set; }

        [Column("requested_by_user_id")]
        public Guid RequestedByUserId { get; set; }

        [Column("request_date")]
        public DateOnly RequestDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        [Column("approval_status")]
        [MaxLength(50)]
        public string ApprovalStatus { get; set; } = "Pending";

        [Column("approved_by_user_id")]
        public Guid? ApprovedByUserId { get; set; }

        [Column("approval_date")]
        public DateOnly? ApprovalDate { get; set; }

        [Column("approval_notes")]
        public string? ApprovalNotes { get; set; }

        [Column("version_affected")]
        [MaxLength(50)]
        public string? VersionAffected { get; set; }

        [Column("actual_impact_on_schedule_days")]
        public int? ActualImpactOnScheduleDays { get; set; }

        [Column("actual_impact_on_budget_usd", TypeName = "numeric(18,2)")] 
        [Range(0, double.MaxValue)]
        public decimal? ActualImpactOnBudgetUsd { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Project? Project { get; set; }
    }
} 