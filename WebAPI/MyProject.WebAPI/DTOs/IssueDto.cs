using System.ComponentModel.DataAnnotations;

namespace MyProject.WebAPI.DTOs
{
    public class IssueDto
    {
        public Guid IssueId { get; set; }
        public Guid ProjectId { get; set; }
        public Guid? TaskId { get; set; }
        public string IssueName { get; set; } = string.Empty;
        public string IssueDescription { get; set; } = string.Empty;
        public string? IssueType { get; set; }
        public string? RootCause { get; set; }
        public int? ImpactOnScheduleDays { get; set; }
        public decimal? ImpactOnBudgetUsd { get; set; }
        public string? ImpactOnScope { get; set; }
        public string? ImpactOnQuality { get; set; }
        public string? ResolutionSteps { get; set; }
        public string Severity { get; set; } = "Medium";
        public string Priority { get; set; } = "Normal";
        public Guid? AssignedToUserId { get; set; }
        public string Status { get; set; } = "Open";
        public DateOnly OpenedDate { get; set; }
        public DateOnly? ClosedDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid TenantId { get; set; }
        public Guid? MilestoneId { get; set; }
        public ProjectDto? Project { get; set; }
        public TaskDto? Task { get; set; }
        public UserDto? AssignedToUser { get; set; }
        public OrganizationDto? Tenant { get; set; }
        public MilestoneDto? Milestone { get; set; }
    }

    public class CreateIssueDto
    {
        [Required]
        public Guid ProjectId { get; set; }

        public Guid? TaskId { get; set; }

        [Required]
        [MaxLength(255)]
        public string IssueName { get; set; } = string.Empty;

        [Required]
        public string IssueDescription { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? IssueType { get; set; }

        public string? RootCause { get; set; }

        [Range(0, int.MaxValue)]
        public int? ImpactOnScheduleDays { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? ImpactOnBudgetUsd { get; set; }

        public string? ImpactOnScope { get; set; }

        public string? ImpactOnQuality { get; set; }

        public string? ResolutionSteps { get; set; }

        [MaxLength(50)]
        public string Severity { get; set; } = "Medium";

        [MaxLength(50)]
        public string Priority { get; set; } = "Normal";

        public Guid? AssignedToUserId { get; set; }

        [MaxLength(50)]
        public string Status { get; set; } = "Open";

        public DateOnly OpenedDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        public DateOnly? ClosedDate { get; set; }

        [Required]
        public Guid TenantId { get; set; }

        public Guid? MilestoneId { get; set; }
    }

    public class UpdateIssueDto
    {
        public Guid? TaskId { get; set; }

        [MaxLength(255)]
        public string? IssueName { get; set; }

        public string? IssueDescription { get; set; }

        [MaxLength(100)]
        public string? IssueType { get; set; }

        public string? RootCause { get; set; }

        [Range(0, int.MaxValue)]
        public int? ImpactOnScheduleDays { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? ImpactOnBudgetUsd { get; set; }

        public string? ImpactOnScope { get; set; }

        public string? ImpactOnQuality { get; set; }

        public string? ResolutionSteps { get; set; }

        [MaxLength(50)]
        public string? Severity { get; set; }

        [MaxLength(50)]
        public string? Priority { get; set; }

        public Guid? AssignedToUserId { get; set; }

        [MaxLength(50)]
        public string? Status { get; set; }

        public DateOnly? OpenedDate { get; set; }

        public DateOnly? ClosedDate { get; set; }

        public Guid? MilestoneId { get; set; }
    }
} 