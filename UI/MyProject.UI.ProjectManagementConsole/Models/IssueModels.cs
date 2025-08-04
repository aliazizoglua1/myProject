using System.ComponentModel.DataAnnotations;

namespace MyProject.UI.ProjectManagementConsole.Models
{
    public class IssueDto
    {
        public Guid IssueId { get; set; }
        public string IssueName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Guid ProjectId { get; set; }
        public Guid? TaskId { get; set; }
        public Guid? AssignedToUserId { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public string Severity { get; set; } = string.Empty;
        public DateOnly? OpenedDate { get; set; }
        public DateOnly? DueDate { get; set; }
        public DateOnly? ResolvedDate { get; set; }
        public string? Resolution { get; set; }
        public string? RootCause { get; set; }
        public string? Notes { get; set; }
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
        [Required(ErrorMessage = "Issue name is required")]
        [StringLength(255, ErrorMessage = "Issue name cannot exceed 255 characters")]
        public string IssueName { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Project ID is required")]
        public Guid ProjectId { get; set; }

        public Guid? TaskId { get; set; }

        public Guid? AssignedToUserId { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters")]
        public string Status { get; set; } = "Open";

        [Required(ErrorMessage = "Priority is required")]
        [StringLength(50, ErrorMessage = "Priority cannot exceed 50 characters")]
        public string Priority { get; set; } = "Medium";

        [Required(ErrorMessage = "Severity is required")]
        [StringLength(50, ErrorMessage = "Severity cannot exceed 50 characters")]
        public string Severity { get; set; } = "Medium";

        [Required(ErrorMessage = "Opened date is required")]
        public DateOnly OpenedDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        public DateOnly? DueDate { get; set; }

        public DateOnly? ResolvedDate { get; set; }

        [StringLength(1000, ErrorMessage = "Resolution cannot exceed 1000 characters")]
        public string? Resolution { get; set; }

        [StringLength(1000, ErrorMessage = "Root cause cannot exceed 1000 characters")]
        public string? RootCause { get; set; }

        [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters")]
        public string? Notes { get; set; }

        [Required(ErrorMessage = "Tenant ID is required")]
        public Guid TenantId { get; set; }

        public Guid? MilestoneId { get; set; }
    }

    public class UpdateIssueDto
    {
        [StringLength(255, ErrorMessage = "Issue name cannot exceed 255 characters")]
        public string? IssueName { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }

        public Guid? TaskId { get; set; }

        public Guid? AssignedToUserId { get; set; }

        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters")]
        public string? Status { get; set; }

        [StringLength(50, ErrorMessage = "Priority cannot exceed 50 characters")]
        public string? Priority { get; set; }

        [StringLength(50, ErrorMessage = "Severity cannot exceed 50 characters")]
        public string? Severity { get; set; }

        public DateOnly? OpenedDate { get; set; }

        public DateOnly? DueDate { get; set; }

        public DateOnly? ResolvedDate { get; set; }

        [StringLength(1000, ErrorMessage = "Resolution cannot exceed 1000 characters")]
        public string? Resolution { get; set; }

        [StringLength(1000, ErrorMessage = "Root cause cannot exceed 1000 characters")]
        public string? RootCause { get; set; }

        [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters")]
        public string? Notes { get; set; }

        public Guid? MilestoneId { get; set; }
    }
} 