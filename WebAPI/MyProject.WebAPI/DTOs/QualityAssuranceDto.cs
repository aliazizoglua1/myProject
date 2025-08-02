using System.ComponentModel.DataAnnotations;

namespace MyProject.WebAPI.DTOs
{
    public class QualityAssuranceDto
    {
        public Guid QaItemId { get; set; }
        public Guid ProjectId { get; set; }
        public Guid? TaskId { get; set; }
        public string ItemType { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Severity { get; set; }
        public string? Priority { get; set; }
        public string Status { get; set; } = "Open";
        public Guid? ReportedByUserId { get; set; }
        public Guid? AssignedToUserId { get; set; }
        public DateOnly ReportedDate { get; set; }
        public DateOnly? ResolutionDate { get; set; }
        public DateOnly? ClosedDate { get; set; }
        public string? ResolutionNotes { get; set; }
        public string? TestEnvironment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ProjectDto? Project { get; set; }
        public TaskDto? Task { get; set; }
    }

    public class CreateQualityAssuranceDto
    {
        [Required]
        public Guid ProjectId { get; set; }

        public Guid? TaskId { get; set; }

        [Required]
        [MaxLength(50)]
        public string ItemType { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Summary { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? Severity { get; set; }

        [MaxLength(50)]
        public string? Priority { get; set; }

        [MaxLength(50)]
        public string Status { get; set; } = "Open";

        public Guid? ReportedByUserId { get; set; }

        public Guid? AssignedToUserId { get; set; }

        public DateOnly ReportedDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        public DateOnly? ResolutionDate { get; set; }

        public DateOnly? ClosedDate { get; set; }

        public string? ResolutionNotes { get; set; }

        public string? TestEnvironment { get; set; }
    }

    public class UpdateQualityAssuranceDto
    {
        public Guid? TaskId { get; set; }

        [MaxLength(50)]
        public string? ItemType { get; set; }

        [MaxLength(255)]
        public string? Summary { get; set; }

        public string? Description { get; set; }

        [MaxLength(50)]
        public string? Severity { get; set; }

        [MaxLength(50)]
        public string? Priority { get; set; }

        [MaxLength(50)]
        public string? Status { get; set; }

        public Guid? ReportedByUserId { get; set; }

        public Guid? AssignedToUserId { get; set; }

        public DateOnly? ResolutionDate { get; set; }

        public DateOnly? ClosedDate { get; set; }

        public string? ResolutionNotes { get; set; }

        public string? TestEnvironment { get; set; }
    }
} 