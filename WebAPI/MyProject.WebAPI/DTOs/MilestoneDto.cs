using System.ComponentModel.DataAnnotations;

namespace MyProject.WebAPI.DTOs
{
    public class MilestoneDto
    {
        public Guid MilestoneId { get; set; }
        public Guid ProjectId { get; set; }
        public string MilestoneName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateOnly DueDate { get; set; }
        public bool IsAchieved { get; set; }
        public DateOnly? CompletionDate { get; set; }
        public Guid TenantId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CreateMilestoneDto
    {
        [Required]
        public Guid ProjectId { get; set; }

        [Required]
        [MaxLength(255)]
        public string MilestoneName { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public DateOnly DueDate { get; set; }

        public bool IsAchieved { get; set; } = false;

        public DateOnly? CompletionDate { get; set; }

        [Required]
        public Guid TenantId { get; set; }
    }

    public class UpdateMilestoneDto
    {
        [MaxLength(255)]
        public string? MilestoneName { get; set; }

        public string? Description { get; set; }

        public DateOnly? DueDate { get; set; }

        public bool? IsAchieved { get; set; }

        public DateOnly? CompletionDate { get; set; }
    }

    public class MarkMilestoneAchievedDto
    {
        public DateOnly? CompletionDate { get; set; }
    }
} 