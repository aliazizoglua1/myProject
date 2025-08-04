using System.ComponentModel.DataAnnotations;

namespace MyProject.UI.ProjectManagementConsole.Models
{
    public class RiskDto
    {
        public Guid RiskId { get; set; }
        public string RiskName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Guid ProjectId { get; set; }
        public Guid? RiskOwnerId { get; set; }
        public string RiskCategory { get; set; } = string.Empty;
        public string RiskStatus { get; set; } = string.Empty;
        public string RiskLevel { get; set; } = string.Empty;
        public int ProbabilityScore { get; set; }
        public int ImpactScore { get; set; }
        public int ExposureScore { get; set; }
        public DateOnly? IdentifiedDate { get; set; }
        public DateOnly? MitigationDate { get; set; }
        public DateOnly? LastReviewDate { get; set; }
        public string? MitigationStrategy { get; set; }
        public string? ContingencyPlan { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid TenantId { get; set; }
        public ProjectDto? Project { get; set; }
        public UserDto? RiskOwner { get; set; }
        public OrganizationDto? Tenant { get; set; }
    }

    public class CreateRiskDto
    {
        [Required(ErrorMessage = "Risk name is required")]
        [StringLength(255, ErrorMessage = "Risk name cannot exceed 255 characters")]
        public string RiskName { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Project ID is required")]
        public Guid ProjectId { get; set; }

        public Guid? RiskOwnerId { get; set; }

        [Required(ErrorMessage = "Risk category is required")]
        [StringLength(100, ErrorMessage = "Risk category cannot exceed 100 characters")]
        public string RiskCategory { get; set; } = string.Empty;

        [Required(ErrorMessage = "Risk status is required")]
        [StringLength(50, ErrorMessage = "Risk status cannot exceed 50 characters")]
        public string RiskStatus { get; set; } = "Open";

        [Required(ErrorMessage = "Risk level is required")]
        [StringLength(50, ErrorMessage = "Risk level cannot exceed 50 characters")]
        public string RiskLevel { get; set; } = "Medium";

        [Required(ErrorMessage = "Probability score is required")]
        [Range(1, 10, ErrorMessage = "Probability score must be between 1 and 10")]
        public int ProbabilityScore { get; set; } = 5;

        [Required(ErrorMessage = "Impact score is required")]
        [Range(1, 10, ErrorMessage = "Impact score must be between 1 and 10")]
        public int ImpactScore { get; set; } = 5;

        [Required(ErrorMessage = "Identified date is required")]
        public DateOnly IdentifiedDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        public DateOnly? MitigationDate { get; set; }

        public DateOnly? LastReviewDate { get; set; }

        [StringLength(1000, ErrorMessage = "Mitigation strategy cannot exceed 1000 characters")]
        public string? MitigationStrategy { get; set; }

        [StringLength(1000, ErrorMessage = "Contingency plan cannot exceed 1000 characters")]
        public string? ContingencyPlan { get; set; }

        [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters")]
        public string? Notes { get; set; }

        [Required(ErrorMessage = "Tenant ID is required")]
        public Guid TenantId { get; set; }
    }

    public class UpdateRiskDto
    {
        [StringLength(255, ErrorMessage = "Risk name cannot exceed 255 characters")]
        public string? RiskName { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }

        public Guid? RiskOwnerId { get; set; }

        [StringLength(100, ErrorMessage = "Risk category cannot exceed 100 characters")]
        public string? RiskCategory { get; set; }

        [StringLength(50, ErrorMessage = "Risk status cannot exceed 50 characters")]
        public string? RiskStatus { get; set; }

        [StringLength(50, ErrorMessage = "Risk level cannot exceed 50 characters")]
        public string? RiskLevel { get; set; }

        [Range(1, 10, ErrorMessage = "Probability score must be between 1 and 10")]
        public int? ProbabilityScore { get; set; }

        [Range(1, 10, ErrorMessage = "Impact score must be between 1 and 10")]
        public int? ImpactScore { get; set; }

        public DateOnly? IdentifiedDate { get; set; }

        public DateOnly? MitigationDate { get; set; }

        public DateOnly? LastReviewDate { get; set; }

        [StringLength(1000, ErrorMessage = "Mitigation strategy cannot exceed 1000 characters")]
        public string? MitigationStrategy { get; set; }

        [StringLength(1000, ErrorMessage = "Contingency plan cannot exceed 1000 characters")]
        public string? ContingencyPlan { get; set; }

        [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters")]
        public string? Notes { get; set; }
    }
} 