using System.ComponentModel.DataAnnotations;

namespace MyProject.WebAPI.DTOs
{
    public class TaskDto
    {
        public Guid TaskId { get; set; }
        public Guid ProjectId { get; set; }
        public string TaskName { get; set; } = string.Empty;
        public string TaskType { get; set; } = string.Empty;
        public Guid? AssignedToUserId { get; set; }
        public Guid? ParentTaskId { get; set; }
        public decimal? PlannedEffortHours { get; set; }
        public decimal? ActualEffortHours { get; set; }
        public DateOnly PlannedStartDate { get; set; }
        public DateOnly? PlannedEndDate { get; set; }
        public DateOnly? ActualStartDate { get; set; }
        public DateOnly? ActualEndDate { get; set; }
        public string TaskStatus { get; set; } = "To Do";
        public string Priority { get; set; } = "Medium";
        public bool IsMilestone { get; set; }
        public string? Description { get; set; }
        public string? Comments { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ProjectDto? Project { get; set; }
        public TaskDto? ParentTask { get; set; }
        public ICollection<TaskDto>? Subtasks { get; set; }
    }

    public class CreateTaskDto
    {
        [Required]
        public Guid ProjectId { get; set; }

        [Required]
        [MaxLength(255)]
        public string TaskName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string TaskType { get; set; } = string.Empty;

        public Guid? AssignedToUserId { get; set; }

        public Guid? ParentTaskId { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? PlannedEffortHours { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? ActualEffortHours { get; set; }

        [Required]
        public DateOnly PlannedStartDate { get; set; }

        public DateOnly? PlannedEndDate { get; set; }

        public DateOnly? ActualStartDate { get; set; }

        public DateOnly? ActualEndDate { get; set; }

        [MaxLength(50)]
        public string TaskStatus { get; set; } = "To Do";

        [MaxLength(50)]
        public string Priority { get; set; } = "Medium";

        public bool IsMilestone { get; set; } = false;

        public string? Description { get; set; }

        public string? Comments { get; set; }
    }

    public class UpdateTaskDto
    {
        [MaxLength(255)]
        public string? TaskName { get; set; }

        [MaxLength(100)]
        public string? TaskType { get; set; }

        public Guid? AssignedToUserId { get; set; }

        public Guid? ParentTaskId { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? PlannedEffortHours { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? ActualEffortHours { get; set; }

        public DateOnly? PlannedStartDate { get; set; }

        public DateOnly? PlannedEndDate { get; set; }

        public DateOnly? ActualStartDate { get; set; }

        public DateOnly? ActualEndDate { get; set; }

        [MaxLength(50)]
        public string? TaskStatus { get; set; }

        [MaxLength(50)]
        public string? Priority { get; set; }

        public bool? IsMilestone { get; set; }

        public string? Description { get; set; }

        public string? Comments { get; set; }
    }
} 