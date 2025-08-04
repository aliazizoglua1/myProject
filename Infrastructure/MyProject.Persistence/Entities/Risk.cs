using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Persistence.Entities
{
    [Table("Risk", Schema = "ai")]
    public class Risk
    {
        [Key]
        [Column("risk_id")]
        public Guid RiskId { get; set; } = Guid.NewGuid();

        [Column("project_id")]
        [Required]
        public Guid ProjectId { get; set; }

        [Column("risk_description")]
        [Required]
        public string RiskDescription { get; set; } = string.Empty;

        [Column("risk_category")]
        [Required]
        [MaxLength(100)]
        public string RiskCategory { get; set; } = string.Empty;

        [Column("planned_probability_score")]
        [Range(1, 5)]
        public int? PlannedProbabilityScore { get; set; }

        [Column("actual_probability_score")]
        [Range(1, 5)]
        public int? ActualProbabilityScore { get; set; }

        [Column("planned_impact_score")]
        [Range(1, 5)]
        public int? PlannedImpactScore { get; set; }

        [Column("actual_impact_score")]
        [Range(1, 5)]
        public int? ActualImpactScore { get; set; }

        [Column("planned_risk_exposure")]
        public int? PlannedRiskExposure { get; set; }

        [Column("actual_risk_exposure")]
        public int? ActualRiskExposure { get; set; }

        [Column("mitigation_strategy")]
        public string? MitigationStrategy { get; set; }

        [Column("contingency_plan")]
        public string? ContingencyPlan { get; set; }

        [Column("trigger_events")]
        public string? TriggerEvents { get; set; }

        [Column("risk_owner_id")]
        public Guid? RiskOwnerId { get; set; }

        [Column("risk_status")]
        [Required]
        [MaxLength(50)]
        public string RiskStatus { get; set; } = "Open";

        [Column("identified_date")]
        [Required]
        public DateOnly IdentifiedDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        [Column("last_review_date")]
        public DateOnly? LastReviewDate { get; set; }

        [Column("closure_date")]
        public DateOnly? ClosureDate { get; set; }

        [Column("outcome_description")]
        public string? OutcomeDescription { get; set; }

        [Column("actual_impact_on_schedule_days")]
        public int? ActualImpactOnScheduleDays { get; set; }

        [Column("actual_impact_on_budget_usd", TypeName = "numeric(18,2)")] 
        public decimal? ActualImpactOnBudgetUsd { get; set; }

        [Column("lessons_learned")]
        public string? LessonsLearned { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [Column("tenant_id")]
        [Required]
        public Guid TenantId { get; set; }

        // Navigation properties
        public Project? Project { get; set; }
        public User? RiskOwner { get; set; }
        public Organization? Tenant { get; set; }
    }
} 