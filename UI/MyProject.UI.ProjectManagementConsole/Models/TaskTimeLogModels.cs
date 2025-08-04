using System.ComponentModel.DataAnnotations;

namespace MyProject.UI.ProjectManagementConsole.Models
{
    public class TaskTimeLogDto
    {
        public Guid TaskTimeLogId { get; set; }
        public Guid TaskId { get; set; }
        public Guid ResourceId { get; set; }
        public DateOnly DateWorked { get; set; }
        public decimal HoursWorked { get; set; }
        public string? Description { get; set; }
        public string? WorkType { get; set; }
        public bool IsBillable { get; set; }
        public decimal? HourlyRate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid TenantId { get; set; }
        public decimal? TotalCost { get; set; }
        
        // Navigation properties
        public TaskDto? Task { get; set; }
        public ResourceDto? Resource { get; set; }
        public OrganizationDto? Tenant { get; set; }
    }

    public class CreateTaskTimeLogDto
    {
        [Required(ErrorMessage = "Task is required")]
        public Guid TaskId { get; set; }

        [Required(ErrorMessage = "Resource is required")]
        public Guid ResourceId { get; set; }

        [Required(ErrorMessage = "Date worked is required")]
        public DateOnly DateWorked { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        [Required(ErrorMessage = "Hours worked is required")]
        [Range(0.01, 24.0, ErrorMessage = "Hours worked must be between 0.01 and 24.0")]
        public decimal HoursWorked { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }

        [StringLength(100, ErrorMessage = "Work type cannot exceed 100 characters")]
        public string? WorkType { get; set; }

        public bool IsBillable { get; set; } = true;

        [Range(0, double.MaxValue, ErrorMessage = "Hourly rate must be positive")]
        public decimal? HourlyRate { get; set; }

        [Required(ErrorMessage = "Tenant is required")]
        public Guid TenantId { get; set; }
    }

    public class UpdateTaskTimeLogDto
    {
        [Range(0.01, 24.0, ErrorMessage = "Hours worked must be between 0.01 and 24.0")]
        public decimal? HoursWorked { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }

        [StringLength(100, ErrorMessage = "Work type cannot exceed 100 characters")]
        public string? WorkType { get; set; }

        public bool? IsBillable { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Hourly rate must be positive")]
        public decimal? HourlyRate { get; set; }
    }

    public class TaskTimeLogSummaryDto
    {
        public Guid TaskId { get; set; }
        public string TaskName { get; set; } = string.Empty;
        public decimal TotalHours { get; set; }
        public decimal BillableHours { get; set; }
        public decimal? TotalCost { get; set; }
        public int LogCount { get; set; }
    }

    public class ResourceTimeLogSummaryDto
    {
        public Guid ResourceId { get; set; }
        public string ResourceName { get; set; } = string.Empty;
        public decimal TotalHours { get; set; }
        public decimal BillableHours { get; set; }
        public decimal? TotalCost { get; set; }
        public int LogCount { get; set; }
    }
} 