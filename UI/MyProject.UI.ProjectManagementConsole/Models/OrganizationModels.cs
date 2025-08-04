using System.ComponentModel.DataAnnotations;

namespace MyProject.UI.ProjectManagementConsole.Models
{
    public class OrganizationDto
    {
        public Guid OrganizationId { get; set; }
        public string OrganizationName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string BillingPlan { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CreateOrganizationDto
    {
        [Required(ErrorMessage = "Organization name is required")]
        [StringLength(255, ErrorMessage = "Organization name cannot exceed 255 characters")]
        public string OrganizationName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Status is required")]
        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters")]
        public string Status { get; set; } = "Active";

        [Required(ErrorMessage = "Billing plan is required")]
        [StringLength(50, ErrorMessage = "Billing plan cannot exceed 50 characters")]
        public string BillingPlan { get; set; } = "Basic";

        [Required(ErrorMessage = "Contact email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(255, ErrorMessage = "Email cannot exceed 255 characters")]
        public string ContactEmail { get; set; } = string.Empty;
    }

    public class UpdateOrganizationDto
    {
        [StringLength(255, ErrorMessage = "Organization name cannot exceed 255 characters")]
        public string? OrganizationName { get; set; }

        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters")]
        public string? Status { get; set; }

        [StringLength(50, ErrorMessage = "Billing plan cannot exceed 50 characters")]
        public string? BillingPlan { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(255, ErrorMessage = "Email cannot exceed 255 characters")]
        public string? ContactEmail { get; set; }
    }
} 