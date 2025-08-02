using System.ComponentModel.DataAnnotations;

namespace MyProject.UI.RazorPages.Models
{
    public class Project
    {
        public Guid ProjectId { get; set; } = Guid.NewGuid();

        [Display(Name = "Project Name")]
        [Required]
        [StringLength(250)]
        public string? ProjectName { get; set; }

        [Display(Name = "Project Type")]
        [StringLength(100)]
        public string? ProjectType { get; set; }

        [Display(Name = "Industry Domain")]
        [StringLength(100)]
        public string? IndustryDomain { get; set; }

        [Display(Name = "Planned Start Date")]
        [DataType(DataType.Date)]
        public DateOnly? PlannedStartDate { get; set; }

        [Display(Name = "Planned End Date")]
        [DataType(DataType.Date)]
        public DateOnly? PlannedEndDate { get; set; }

        [Display(Name = "Actual Start Date")]
        [DataType(DataType.Date)]
        public DateOnly? ActualStartDate { get; set; }

        [Display(Name = "Actual End Date")]
        [DataType(DataType.Date)]
        public DateOnly? ActualEndDate { get; set; }

        [Display(Name = "Planned Budget")]
        [DataType(DataType.Currency)]
        public decimal? PlannedBudget { get; set; }

        [Display(Name = "Actual Budget")]
        [DataType(DataType.Currency)]
        public decimal? ActualBudget { get; set; }

        [Display(Name = "Project Manager ID")]
        public Guid? ProjectManagerId { get; set; }

        [Display(Name = "Team Size")]
        public int? TeamSize { get; set; }

        [Display(Name = "Contract Type")]
        [StringLength(50)]
        public string? ContractType { get; set; }

        [Display(Name = "Project Status")]
        [Required]
        [StringLength(50)]
        public string ProjectStatus { get; set; } = "planned";

        [Display(Name = "Project Complexity")]
        [StringLength(50)]
        public string? ProjectComplexity { get; set; }

        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Display(Name = "Updated At")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [Display(Name = "Notes")]
        [DataType(DataType.MultilineText)]
        public string? Notes { get; set; }

        [Display(Name = "Current Phase")]
        [StringLength(100)]
        public string? CurrentPhase { get; set; }

        [Display(Name = "Organization ID")]
        public Guid? OrganizationId { get; set; }
    }
}