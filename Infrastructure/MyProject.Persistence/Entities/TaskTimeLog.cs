using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Persistence.Entities
{
    [Table("TaskTimeLog", Schema = "ai")]
    public class TaskTimeLog
    {
        [Key]
        [Column("task_time_log_id")]
        public Guid TaskTimeLogId { get; set; } = Guid.NewGuid();

        [Column("task_id")]
        [Required]
        public Guid TaskId { get; set; }

        [Column("resource_id")]
        [Required]
        public Guid ResourceId { get; set; }

        [Column("date_worked")]
        [Required]
        public DateOnly DateWorked { get; set; }

        [Column("hours_worked", TypeName = "numeric(8,2)")]
        [Required]
        [Range(0.01, 24.0, ErrorMessage = "Hours worked must be between 0.01 and 24.0")]
        public decimal HoursWorked { get; set; }

        [Column("description")]
        [MaxLength(1000)]
        public string? Description { get; set; }

        [Column("work_type")]
        [MaxLength(100)]
        public string? WorkType { get; set; }

        [Column("is_billable")]
        public bool IsBillable { get; set; } = true;

        [Column("hourly_rate", TypeName = "numeric(10,2)")]
        [Range(0, double.MaxValue)]
        public decimal? HourlyRate { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [Column("tenant_id")]
        [Required]
        public Guid TenantId { get; set; }

        // Navigation properties
        public Task? Task { get; set; }
        public Resource? Resource { get; set; }
        public Organization? Tenant { get; set; }

        // Computed property for total cost
        public decimal? TotalCost => HourlyRate.HasValue ? HoursWorked * HourlyRate.Value : null;
    }
} 