using System.ComponentModel.DataAnnotations;

namespace MyProject.UI.ProjectManagementAdminPanel.Models
{
    public class OrganizationDto
    {
        public Guid OrganizationId { get; set; }
        public string OrganizationName { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string BillingPlan { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CreateOrganizationDto
    {
        [Required(ErrorMessage = "Organization name is required")]
        [StringLength(255, ErrorMessage = "Organization name must not exceed 255 characters")]
        public string OrganizationName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Contact email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [StringLength(255, ErrorMessage = "Email must not exceed 255 characters")]
        public string ContactEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; } = "Active";

        [Required(ErrorMessage = "Billing plan is required")]
        public string BillingPlan { get; set; } = "Basic";
    }

    public class UpdateOrganizationDto
    {
        [Required(ErrorMessage = "Organization name is required")]
        [StringLength(255, ErrorMessage = "Organization name must not exceed 255 characters")]
        public string OrganizationName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Contact email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [StringLength(255, ErrorMessage = "Email must not exceed 255 characters")]
        public string ContactEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; } = string.Empty;

        [Required(ErrorMessage = "Billing plan is required")]
        public string BillingPlan { get; set; } = string.Empty;
    }
}