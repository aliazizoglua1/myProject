using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Persistence.Entities
{
    [Table("Milestone", Schema = "ai")]
    public class Milestone
    {
        [Key]
        [Column("milestone_id")]
        public Guid MilestoneId { get; set; } = Guid.NewGuid();

        [Column("project_id")]
        [Required]
        public Guid ProjectId { get; set; }

        [Column("milestone_name")]
        [Required]
        [MaxLength(255)]
        public string MilestoneName { get; set; } = string.Empty;

        [Column("description")]
        public string? Description { get; set; }

        [Column("due_date")]
        [Required]
        public DateOnly DueDate { get; set; }

        [Column("is_achieved")]
        public bool IsAchieved { get; set; } = false;

        [Column("completion_date")]
        public DateOnly? CompletionDate { get; set; }

        [Column("organization_id")]
        [Required]
        public Guid OrganizationId { get; set; }

        [Column("created_at", TypeName = "timestamp with time zone")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at", TypeName = "timestamp with time zone")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Project? Project { get; set; }
        public Organization? Organization { get; set; }
        public ICollection<Task>? Tasks { get; set; }
        public ICollection<Issue>? Issues { get; set; }
        public ICollection<ChangeRequest>? ChangeRequests { get; set; }
    }
} 