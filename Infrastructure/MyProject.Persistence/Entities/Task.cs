using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Persistence.Entities
{
    [Table("Task", Schema = "ai")]
    public class Task
    {
        [Key]
        [Column("task_id")]
        public Guid TaskId { get; set; } = Guid.NewGuid();

        [Column("project_id")]
        [Required]
        public Guid ProjectId { get; set; }

        [Column("task_name")]
        [Required]
        [MaxLength(255)]
        public string TaskName { get; set; } = string.Empty;

        [Column("task_type")]
        [Required]
        [MaxLength(100)]
        public string TaskType { get; set; } = string.Empty;

        [Column("assigned_to_user_id")]
        public Guid? AssignedToUserId { get; set; }

        [Column("parent_task_id")]
        public Guid? ParentTaskId { get; set; }

        [Column("planned_effort_hours", TypeName = "numeric(8,2)")]
    
        [Range(0, double.MaxValue)]
        public decimal? PlannedEffortHours { get; set; }

        [Column("actual_effort_hours", TypeName = "numeric(8,2)")]
     
        [Range(0, double.MaxValue)]
        public decimal? ActualEffortHours { get; set; }

        [Column("planned_start_date")] 
        public DateOnly? PlannedStartDate { get; set; }

        [Column("planned_end_date")]
        public DateOnly? PlannedEndDate { get; set; }

        [Column("actual_start_date")]
        public DateOnly? ActualStartDate { get; set; }

        [Column("actual_end_date")]
        public DateOnly? ActualEndDate { get; set; }

        [Column("task_status")]
        [Required]
        [MaxLength(50)]
        public string TaskStatus { get; set; } = "To Do";

        [Column("priority")]
        [Required]
        [MaxLength(50)]
        public string Priority { get; set; } = "Medium";

        [Column("is_milestone")]
        public bool IsMilestone { get; set; } = false;

        [Column("description")]
        public string? Description { get; set; }

        [Column("comments")]
        public string? Comments { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [Column("tenant_id")]
        [Required]
        public Guid TenantId { get; set; }

        [Column("milestone_id")]
        public Guid? MilestoneId { get; set; }

        [Column("issue_id")]
        public Guid? IssueId { get; set; }

        // Navigation properties
        public Project? Project { get; set; }
        public Task? ParentTask { get; set; }
        public ICollection<Task>? Subtasks { get; set; }
        public User? AssignedToUser { get; set; }
        public Organization? Tenant { get; set; }
        public Milestone? Milestone { get; set; }
        public Issue? Issue { get; set; }
    }
} 