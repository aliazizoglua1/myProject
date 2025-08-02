using System.ComponentModel.DataAnnotations;

namespace MyProject.WebAPI.DTOs
{
    public class ChangeRequestDto
    {
        public Guid ChangeRequestId { get; set; }
        public Guid ProjectId { get; set; }
        public string RequestTitle { get; set; } = string.Empty;
        public string RequestDescription { get; set; } = string.Empty;
        public string ReasonForChange { get; set; } = string.Empty;
        public string? EstimatedImpactOnScope { get; set; }
        public int? EstimatedImpactOnScheduleDays { get; set; }
        public decimal? EstimatedImpactOnBudgetUsd { get; set; }
        public string? EstimatedImpactOnQuality { get; set; }
        public Guid RequestedByUserId { get; set; }
        public DateOnly RequestDate { get; set; }
        public string ApprovalStatus { get; set; } = "Pending";
        public Guid? ApprovedByUserId { get; set; }
        public DateOnly? ApprovalDate { get; set; }
        public string? ApprovalNotes { get; set; }
        public string? VersionAffected { get; set; }
        public int? ActualImpactOnScheduleDays { get; set; }
        public decimal? ActualImpactOnBudgetUsd { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ProjectDto? Project { get; set; }
    }

    public class CreateChangeRequestDto
    {
        [Required]
        public Guid ProjectId { get; set; }

        [Required]
        [MaxLength(255)]
        public string RequestTitle { get; set; } = string.Empty;

        [Required]
        public string RequestDescription { get; set; } = string.Empty;

        [Required]
        public string ReasonForChange { get; set; } = string.Empty;

        public string? EstimatedImpactOnScope { get; set; }

        [Range(0, int.MaxValue)]
        public int? EstimatedImpactOnScheduleDays { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? EstimatedImpactOnBudgetUsd { get; set; }

        public string? EstimatedImpactOnQuality { get; set; }

        [Required]
        public Guid RequestedByUserId { get; set; }

        public DateOnly RequestDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        [MaxLength(50)]
        public string ApprovalStatus { get; set; } = "Pending";

        public Guid? ApprovedByUserId { get; set; }

        public DateOnly? ApprovalDate { get; set; }

        public string? ApprovalNotes { get; set; }

        [MaxLength(50)]
        public string? VersionAffected { get; set; }

        [Range(0, int.MaxValue)]
        public int? ActualImpactOnScheduleDays { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? ActualImpactOnBudgetUsd { get; set; }
    }

    public class UpdateChangeRequestDto
    {
        [MaxLength(255)]
        public string? RequestTitle { get; set; }

        public string? RequestDescription { get; set; }

        public string? ReasonForChange { get; set; }

        public string? EstimatedImpactOnScope { get; set; }

        [Range(0, int.MaxValue)]
        public int? EstimatedImpactOnScheduleDays { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? EstimatedImpactOnBudgetUsd { get; set; }

        public string? EstimatedImpactOnQuality { get; set; }

        [MaxLength(50)]
        public string? ApprovalStatus { get; set; }

        public Guid? ApprovedByUserId { get; set; }

        public DateOnly? ApprovalDate { get; set; }

        public string? ApprovalNotes { get; set; }

        [MaxLength(50)]
        public string? VersionAffected { get; set; }

        [Range(0, int.MaxValue)]
        public int? ActualImpactOnScheduleDays { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? ActualImpactOnBudgetUsd { get; set; }
    }
} 