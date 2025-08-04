using System.ComponentModel.DataAnnotations;

namespace MyProject.UI.ProjectManagementConsole.Models
{
    public class ProjectDto
    {
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public string ProjectType { get; set; } = string.Empty;
        public string IndustryDomain { get; set; } = string.Empty;
        public DateOnly? PlannedStartDate { get; set; }
        public DateOnly? PlannedEndDate { get; set; }
        public DateOnly? ActualStartDate { get; set; }
        public DateOnly? ActualEndDate { get; set; }
        public decimal? PlannedBudget { get; set; }
        public decimal? ActualBudget { get; set; }
        public Guid? ProjectManagerId { get; set; }
        public int? TeamSize { get; set; }
        public string ContractType { get; set; } = string.Empty;
        public string ProjectStatus { get; set; } = string.Empty;
        public string ProjectComplexity { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? Description { get; set; }
        public string? Notes { get; set; }
        public string? CurrentPhase { get; set; }
        public Guid? OrganizationId { get; set; }
        public OrganizationDto? Organization { get; set; }
        public UserDto? ProjectManager { get; set; }
    }

    public class CreateProjectDto
    {
        [Required(ErrorMessage = "Project name is required")]
        [StringLength(255, ErrorMessage = "Project name cannot exceed 255 characters")]
        public string ProjectName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Project type is required")]
        [StringLength(100, ErrorMessage = "Project type cannot exceed 100 characters")]
        public string ProjectType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Industry domain is required")]
        [StringLength(100, ErrorMessage = "Industry domain cannot exceed 100 characters")]
        public string IndustryDomain { get; set; } = string.Empty;

        public DateOnly? PlannedStartDate { get; set; }

        public DateOnly? PlannedEndDate { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Planned budget must be non-negative")]
        public decimal? PlannedBudget { get; set; }

        public Guid? ProjectManagerId { get; set; }

        [Range(1, 1000, ErrorMessage = "Team size must be between 1 and 1000")]
        public int? TeamSize { get; set; }

        [StringLength(50, ErrorMessage = "Contract type cannot exceed 50 characters")]
        public string ContractType { get; set; } = "Fixed Price";

        [StringLength(50, ErrorMessage = "Project status cannot exceed 50 characters")]
        public string ProjectStatus { get; set; } = "Planning";

        [StringLength(50, ErrorMessage = "Project complexity cannot exceed 50 characters")]
        public string ProjectComplexity { get; set; } = "Medium";

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }

        [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters")]
        public string? Notes { get; set; }

        [StringLength(100, ErrorMessage = "Current phase cannot exceed 100 characters")]
        public string? CurrentPhase { get; set; }

        public Guid? OrganizationId { get; set; }
    }

    public class UpdateProjectDto
    {
        [StringLength(255, ErrorMessage = "Project name cannot exceed 255 characters")]
        public string? ProjectName { get; set; }

        [StringLength(100, ErrorMessage = "Project type cannot exceed 100 characters")]
        public string? ProjectType { get; set; }

        [StringLength(100, ErrorMessage = "Industry domain cannot exceed 100 characters")]
        public string? IndustryDomain { get; set; }

        public DateOnly? PlannedStartDate { get; set; }

        public DateOnly? PlannedEndDate { get; set; }

        public DateOnly? ActualStartDate { get; set; }

        public DateOnly? ActualEndDate { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Planned budget must be non-negative")]
        public decimal? PlannedBudget { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Actual budget must be non-negative")]
        public decimal? ActualBudget { get; set; }

        public Guid? ProjectManagerId { get; set; }

        [Range(1, 1000, ErrorMessage = "Team size must be between 1 and 1000")]
        public int? TeamSize { get; set; }

        [StringLength(50, ErrorMessage = "Contract type cannot exceed 50 characters")]
        public string? ContractType { get; set; }

        [StringLength(50, ErrorMessage = "Project status cannot exceed 50 characters")]
        public string? ProjectStatus { get; set; }

        [StringLength(50, ErrorMessage = "Project complexity cannot exceed 50 characters")]
        public string? ProjectComplexity { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }

        [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters")]
        public string? Notes { get; set; }

        [StringLength(100, ErrorMessage = "Current phase cannot exceed 100 characters")]
        public string? CurrentPhase { get; set; }
    }
} 