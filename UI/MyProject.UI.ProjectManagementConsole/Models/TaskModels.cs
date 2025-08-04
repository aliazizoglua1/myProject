using System.ComponentModel.DataAnnotations;

namespace MyProject.UI.ProjectManagementConsole.Models
{
    public class TaskDto
    {
        public Guid TaskId { get; set; }
        public string TaskName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Guid ProjectId { get; set; }
        public Guid? ParentTaskId { get; set; }
        public Guid? AssignedToUserId { get; set; }
        public DateOnly? PlannedStartDate { get; set; }
        public DateOnly? PlannedEndDate { get; set; }
        public DateOnly? ActualStartDate { get; set; }
        public DateOnly? ActualEndDate { get; set; }
        public string TaskStatus { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public decimal? PlannedEffort { get; set; }
        public decimal? ActualEffort { get; set; }
        public decimal? ProgressPercentage { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid TenantId { get; set; }
        public Guid? MilestoneId { get; set; }
        public ProjectDto? Project { get; set; }
        public TaskDto? ParentTask { get; set; }
        public ICollection<TaskDto>? Subtasks { get; set; }
        public UserDto? AssignedToUser { get; set; }
        public OrganizationDto? Tenant { get; set; }
        public MilestoneDto? Milestone { get; set; }
    }

    public class CreateTaskDto
    {
        [Required(ErrorMessage = "Task name is required")]
        [StringLength(255, ErrorMessage = "Task name cannot exceed 255 characters")]
        public string TaskName { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Project ID is required")]
        public Guid ProjectId { get; set; }

        public Guid? ParentTaskId { get; set; }

        public Guid? AssignedToUserId { get; set; }

        [Required(ErrorMessage = "Planned start date is required")]
        public DateOnly PlannedStartDate { get; set; }

        public DateOnly? PlannedEndDate { get; set; }

        public DateOnly? ActualStartDate { get; set; }

        public DateOnly? ActualEndDate { get; set; }

        [Required(ErrorMessage = "Task status is required")]
        [StringLength(50, ErrorMessage = "Task status cannot exceed 50 characters")]
        public string TaskStatus { get; set; } = "To Do";

        [Required(ErrorMessage = "Priority is required")]
        [StringLength(50, ErrorMessage = "Priority cannot exceed 50 characters")]
        public string Priority { get; set; } = "Medium";

        [Range(0, double.MaxValue, ErrorMessage = "Planned effort must be non-negative")]
        public decimal? PlannedEffort { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Actual effort must be non-negative")]
        public decimal? ActualEffort { get; set; }

        [Range(0, 100, ErrorMessage = "Progress percentage must be between 0 and 100")]
        public decimal? ProgressPercentage { get; set; }

        [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters")]
        public string? Notes { get; set; }

        [Required(ErrorMessage = "Tenant ID is required")]
        public Guid TenantId { get; set; }

        public Guid? MilestoneId { get; set; }
    }

    public class UpdateTaskDto
    {
        [StringLength(255, ErrorMessage = "Task name cannot exceed 255 characters")]
        public string? TaskName { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }

        public Guid? ParentTaskId { get; set; }

        public Guid? AssignedToUserId { get; set; }

        public DateOnly? PlannedStartDate { get; set; }

        public DateOnly? PlannedEndDate { get; set; }

        public DateOnly? ActualStartDate { get; set; }

        public DateOnly? ActualEndDate { get; set; }

        [StringLength(50, ErrorMessage = "Task status cannot exceed 50 characters")]
        public string? TaskStatus { get; set; }

        [StringLength(50, ErrorMessage = "Priority cannot exceed 50 characters")]
        public string? Priority { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Planned effort must be non-negative")]
        public decimal? PlannedEffort { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Actual effort must be non-negative")]
        public decimal? ActualEffort { get; set; }

        [Range(0, 100, ErrorMessage = "Progress percentage must be between 0 and 100")]
        public decimal? ProgressPercentage { get; set; }

        [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters")]
        public string? Notes { get; set; }

        public Guid? MilestoneId { get; set; }
    }
} 