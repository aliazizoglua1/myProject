using System.ComponentModel.DataAnnotations;

namespace MyProject.UI.ProjectManagementConsole.Models
{
    public class ChangeRequestDto
    {
        public Guid ChangeRequestId { get; set; }
        public string ChangeRequestTitle { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Guid ProjectId { get; set; }
        public Guid RequestedByUserId { get; set; }
        public Guid? ApprovedByUserId { get; set; }
        public DateOnly RequestDate { get; set; }
        public DateOnly? ApprovalDate { get; set; }
        public string ApprovalStatus { get; set; } = string.Empty;
        public string ChangeType { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public string? BusinessJustification { get; set; }
        public string? TechnicalImpact { get; set; }
        public string? RiskAssessment { get; set; }
        public string? ImplementationPlan { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid TenantId { get; set; }
        public Guid? MilestoneId { get; set; }
        public ProjectDto? Project { get; set; }
        public UserDto? RequestedByUser { get; set; }
        public UserDto? ApprovedByUser { get; set; }
        public OrganizationDto? Tenant { get; set; }
        public MilestoneDto? Milestone { get; set; }
    }

    public class CreateChangeRequestDto
    {
        [Required(ErrorMessage = "Change request title is required")]
        [StringLength(255, ErrorMessage = "Change request title cannot exceed 255 characters")]
        public string ChangeRequestTitle { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Project ID is required")]
        public Guid ProjectId { get; set; }

        [Required(ErrorMessage = "Requested by user ID is required")]
        public Guid RequestedByUserId { get; set; }

        public Guid? ApprovedByUserId { get; set; }

        [Required(ErrorMessage = "Request date is required")]
        public DateOnly RequestDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        public DateOnly? ApprovalDate { get; set; }

        [Required(ErrorMessage = "Approval status is required")]
        [StringLength(50, ErrorMessage = "Approval status cannot exceed 50 characters")]
        public string ApprovalStatus { get; set; } = "Pending";

        [Required(ErrorMessage = "Change type is required")]
        [StringLength(100, ErrorMessage = "Change type cannot exceed 100 characters")]
        public string ChangeType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Priority is required")]
        [StringLength(50, ErrorMessage = "Priority cannot exceed 50 characters")]
        public string Priority { get; set; } = "Medium";

        [StringLength(1000, ErrorMessage = "Business justification cannot exceed 1000 characters")]
        public string? BusinessJustification { get; set; }

        [StringLength(1000, ErrorMessage = "Technical impact cannot exceed 1000 characters")]
        public string? TechnicalImpact { get; set; }

        [StringLength(1000, ErrorMessage = "Risk assessment cannot exceed 1000 characters")]
        public string? RiskAssessment { get; set; }

        [StringLength(1000, ErrorMessage = "Implementation plan cannot exceed 1000 characters")]
        public string? ImplementationPlan { get; set; }

        [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters")]
        public string? Notes { get; set; }

        [Required(ErrorMessage = "Tenant ID is required")]
        public Guid TenantId { get; set; }

        public Guid? MilestoneId { get; set; }
    }

    public class UpdateChangeRequestDto
    {
        [StringLength(255, ErrorMessage = "Change request title cannot exceed 255 characters")]
        public string? ChangeRequestTitle { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }

        public Guid? ApprovedByUserId { get; set; }

        public DateOnly? ApprovalDate { get; set; }

        [StringLength(50, ErrorMessage = "Approval status cannot exceed 50 characters")]
        public string? ApprovalStatus { get; set; }

        [StringLength(100, ErrorMessage = "Change type cannot exceed 100 characters")]
        public string? ChangeType { get; set; }

        [StringLength(50, ErrorMessage = "Priority cannot exceed 50 characters")]
        public string? Priority { get; set; }

        [StringLength(1000, ErrorMessage = "Business justification cannot exceed 1000 characters")]
        public string? BusinessJustification { get; set; }

        [StringLength(1000, ErrorMessage = "Technical impact cannot exceed 1000 characters")]
        public string? TechnicalImpact { get; set; }

        [StringLength(1000, ErrorMessage = "Risk assessment cannot exceed 1000 characters")]
        public string? RiskAssessment { get; set; }

        [StringLength(1000, ErrorMessage = "Implementation plan cannot exceed 1000 characters")]
        public string? ImplementationPlan { get; set; }

        [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters")]
        public string? Notes { get; set; }

        public Guid? MilestoneId { get; set; }
    }
} 