using System.ComponentModel.DataAnnotations;

namespace MyProject.UI.ProjectManagementConsole.Models
{
    public class MilestoneDto
    {
        public Guid MilestoneId { get; set; }
        public string MilestoneName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Guid ProjectId { get; set; }
        public DateOnly? TargetDate { get; set; }
        public DateOnly? AchievedDate { get; set; }
        public bool IsAchieved { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid TenantId { get; set; }
        public ProjectDto? Project { get; set; }
        public OrganizationDto? Tenant { get; set; }
    }

    public class CreateMilestoneDto
    {
        [Required(ErrorMessage = "Milestone name is required")]
        [StringLength(255, ErrorMessage = "Milestone name cannot exceed 255 characters")]
        public string MilestoneName { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Project ID is required")]
        public Guid ProjectId { get; set; }

        [Required(ErrorMessage = "Target date is required")]
        public DateOnly TargetDate { get; set; }

        public DateOnly? AchievedDate { get; set; }

        public bool IsAchieved { get; set; } = false;

        [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters")]
        public string? Notes { get; set; }

        [Required(ErrorMessage = "Tenant ID is required")]
        public Guid TenantId { get; set; }
    }

    public class UpdateMilestoneDto
    {
        [StringLength(255, ErrorMessage = "Milestone name cannot exceed 255 characters")]
        public string? MilestoneName { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }

        public DateOnly? TargetDate { get; set; }

        public DateOnly? AchievedDate { get; set; }

        public bool? IsAchieved { get; set; }

        [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters")]
        public string? Notes { get; set; }
    }

    public class MarkMilestoneAchievedDto
    {
        [Required(ErrorMessage = "Achieved date is required")]
        public DateOnly AchievedDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters")]
        public string? Notes { get; set; }
    }
} 