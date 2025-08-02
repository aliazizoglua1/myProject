using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Persistence.Entities
{
    [Table("QualityAssurance", Schema = "ai")]
    public class QualityAssurance
    {
        [Key]
        [Column("qa_item_id")]
        public Guid QaItemId { get; set; } = Guid.NewGuid();

        [Column("project_id")]
        public Guid ProjectId { get; set; }

        [Column("task_id")]
        public Guid? TaskId { get; set; }

        [Column("item_type")]
        [Required]
        [MaxLength(50)]
        public string ItemType { get; set; } = string.Empty;

        [Column("summary")]
        [Required]
        [MaxLength(255)]
        public string Summary { get; set; } = string.Empty;

        [Column("description")]
        [Required]
        public string Description { get; set; } = string.Empty;

        [Column("severity")]
        [MaxLength(50)]
        public string? Severity { get; set; }

        [Column("priority")]
        [MaxLength(50)]
        public string? Priority { get; set; }

        [Column("status")]
        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Open";

        [Column("reported_by_user_id")]
        public Guid? ReportedByUserId { get; set; }

        [Column("assigned_to_user_id")]
        public Guid? AssignedToUserId { get; set; }

        [Column("reported_date")]
        public DateOnly ReportedDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        [Column("resolution_date")]
        public DateOnly? ResolutionDate { get; set; }

        [Column("closed_date")]
        public DateOnly? ClosedDate { get; set; }

        [Column("resolution_notes")]
        public string? ResolutionNotes { get; set; }

        [Column("test_environment")]
        public string? TestEnvironment { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Project? Project { get; set; }
        public Task? Task { get; set; }
    }
} 