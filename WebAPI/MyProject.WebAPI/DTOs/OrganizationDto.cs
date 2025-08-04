using System.ComponentModel.DataAnnotations;

namespace MyProject.WebAPI.DTOs
{
    public class OrganizationDto
    {
        public Guid OrganizationId { get; set; }
        public string OrganizationName { get; set; } = string.Empty;
        public string Status { get; set; } = "Active";
        public string? BillingPlan { get; set; }
        public string? ContactEmail { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CreateOrganizationDto
    {
        [Required]
        [MaxLength(255)]
        public string OrganizationName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Status { get; set; } = "Active";

        [MaxLength(50)]
        public string? BillingPlan { get; set; }

        [MaxLength(255)]
        [EmailAddress]
        public string? ContactEmail { get; set; }
    }

    public class UpdateOrganizationDto
    {
        [MaxLength(255)]
        public string? OrganizationName { get; set; }

        [MaxLength(50)]
        public string? Status { get; set; }

        [MaxLength(50)]
        public string? BillingPlan { get; set; }

        [MaxLength(255)]
        [EmailAddress]
        public string? ContactEmail { get; set; }
    }
} 