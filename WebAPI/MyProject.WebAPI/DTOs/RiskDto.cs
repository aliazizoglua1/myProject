using System.ComponentModel.DataAnnotations;

namespace MyProject.WebAPI.DTOs
{
    public class RiskDto
    {
        public Guid RiskId { get; set; }
        public Guid ProjectId { get; set; }
        public string RiskDescription { get; set; } = string.Empty;
        public string RiskCategory { get; set; } = string.Empty;
        public int? PlannedProbabilityScore { get; set; }
        public int? ActualProbabilityScore { get; set; }
        public int? PlannedImpactScore { get; set; }
        public int? ActualImpactScore { get; set; }
        public int? PlannedRiskExposure { get; set; }
        public int? ActualRiskExposure { get; set; }
        public string? MitigationStrategy { get; set; }
        public string? ContingencyPlan { get; set; }
        public string? TriggerEvents { get; set; }
        public Guid? RiskOwnerId { get; set; }
        public string RiskStatus { get; set; } = "Open";
        public DateOnly IdentifiedDate { get; set; }
        public DateOnly? LastReviewDate { get; set; }
        public DateOnly? ClosureDate { get; set; }
        public string? OutcomeDescription { get; set; }
        public int? ActualImpactOnScheduleDays { get; set; }
        public decimal? ActualImpactOnBudgetUsd { get; set; }
        public string? LessonsLearned { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ProjectDto? Project { get; set; }
    }

    public class CreateRiskDto
    {
        [Required]
        public Guid ProjectId { get; set; }

        [Required]
        public string RiskDescription { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string RiskCategory { get; set; } = string.Empty;

        [Range(1, 5)]
        public int? PlannedProbabilityScore { get; set; }

        [Range(1, 5)]
        public int? ActualProbabilityScore { get; set; }

        [Range(1, 5)]
        public int? PlannedImpactScore { get; set; }

        [Range(1, 5)]
        public int? ActualImpactScore { get; set; }

        public string? MitigationStrategy { get; set; }

        public string? ContingencyPlan { get; set; }

        public string? TriggerEvents { get; set; }

        public Guid? RiskOwnerId { get; set; }

        [MaxLength(50)]
        public string RiskStatus { get; set; } = "Open";

        public DateOnly? IdentifiedDate { get; set; }

        public DateOnly? LastReviewDate { get; set; }

        public DateOnly? ClosureDate { get; set; }

        public string? OutcomeDescription { get; set; }

        public int? ActualImpactOnScheduleDays { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? ActualImpactOnBudgetUsd { get; set; }

        public string? LessonsLearned { get; set; }
    }

    public class UpdateRiskDto
    {
        public string? RiskDescription { get; set; }

        [MaxLength(100)]
        public string? RiskCategory { get; set; }

        [Range(1, 5)]
        public int? PlannedProbabilityScore { get; set; }

        [Range(1, 5)]
        public int? ActualProbabilityScore { get; set; }

        [Range(1, 5)]
        public int? PlannedImpactScore { get; set; }

        [Range(1, 5)]
        public int? ActualImpactScore { get; set; }

        public string? MitigationStrategy { get; set; }

        public string? ContingencyPlan { get; set; }

        public string? TriggerEvents { get; set; }

        public Guid? RiskOwnerId { get; set; }

        [MaxLength(50)]
        public string? RiskStatus { get; set; }

        public DateOnly? IdentifiedDate { get; set; }

        public DateOnly? LastReviewDate { get; set; }

        public DateOnly? ClosureDate { get; set; }

        public string? OutcomeDescription { get; set; }

        public int? ActualImpactOnScheduleDays { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? ActualImpactOnBudgetUsd { get; set; }

        public string? LessonsLearned { get; set; }
    }
} 