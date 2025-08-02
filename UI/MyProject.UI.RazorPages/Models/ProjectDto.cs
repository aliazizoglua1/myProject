using System.ComponentModel.DataAnnotations;

namespace MyProject.UI.RazorPages.Models
{
    public class ProjectDto
    {
        public Guid ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public string? ProjectType { get; set; }
        public string? IndustryDomain { get; set; }
        public DateOnly? PlannedStartDate { get; set; }
        public DateOnly? PlannedEndDate { get; set; }
        public DateOnly? ActualStartDate { get; set; }
        public DateOnly? ActualEndDate { get; set; }
        public decimal? PlannedBudget { get; set; }
        public decimal? ActualBudget { get; set; }
        public Guid? ProjectManagerId { get; set; }
        public int? TeamSize { get; set; }
        public string? ContractType { get; set; }
        public string ProjectStatus { get; set; } = "planned";
        public string? ProjectComplexity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? Description { get; set; }
        public string? Notes { get; set; }
        public string? CurrentPhase { get; set; }
        public Guid? OrganizationId { get; set; }
    }

    public class CreateProjectDto
    {
        [Required]
        [MaxLength(250)]
        public string ProjectName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? ProjectType { get; set; }

        [MaxLength(100)]
        public string? IndustryDomain { get; set; }

        public DateOnly? PlannedStartDate { get; set; }

        public DateOnly? PlannedEndDate { get; set; }

        public DateOnly? ActualStartDate { get; set; }

        public DateOnly? ActualEndDate { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? PlannedBudget { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? ActualBudget { get; set; }

        public Guid? ProjectManagerId { get; set; }

        [Range(1, int.MaxValue)]
        public int? TeamSize { get; set; }

        [MaxLength(50)]
        public string? ContractType { get; set; }

        [MaxLength(50)]
        public string ProjectStatus { get; set; } = "planned";

        [MaxLength(50)]
        public string? ProjectComplexity { get; set; }

        public string? Description { get; set; }

        public string? Notes { get; set; }

        [MaxLength(100)]
        public string? CurrentPhase { get; set; }

        public Guid? OrganizationId { get; set; }
    }

    public class UpdateProjectDto
    {
        [MaxLength(250)]
        public string? ProjectName { get; set; }

        [MaxLength(100)]
        public string? ProjectType { get; set; }

        [MaxLength(100)]
        public string? IndustryDomain { get; set; }

        public DateOnly? PlannedStartDate { get; set; }

        public DateOnly? PlannedEndDate { get; set; }

        public DateOnly? ActualStartDate { get; set; }

        public DateOnly? ActualEndDate { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? PlannedBudget { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? ActualBudget { get; set; }

        public Guid? ProjectManagerId { get; set; }

        [Range(1, int.MaxValue)]
        public int? TeamSize { get; set; }

        [MaxLength(50)]
        public string? ContractType { get; set; }

        [MaxLength(50)]
        public string? ProjectStatus { get; set; }

        [MaxLength(50)]
        public string? ProjectComplexity { get; set; }

        public string? Description { get; set; }

        public string? Notes { get; set; }

        [MaxLength(100)]
        public string? CurrentPhase { get; set; }

        public Guid? OrganizationId { get; set; }
    }
}