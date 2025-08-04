using System.ComponentModel.DataAnnotations;

namespace MyProject.UI.ProjectManagementConsole.Models
{
    public class ResourceDto
    {
        public Guid ResourceId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string EmploymentStatus { get; set; } = string.Empty;
        public DateOnly? HireDate { get; set; }
        public DateOnly? TerminationDate { get; set; }
        public decimal? Salary { get; set; }
        public string? Skills { get; set; }
        public string? Certifications { get; set; }
        public string? Experience { get; set; }
        public string? Education { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
        public UserDto? User { get; set; }
        public OrganizationDto? Tenant { get; set; }
    }

    public class CreateResourceDto
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(100, ErrorMessage = "First name cannot exceed 100 characters")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(100, ErrorMessage = "Last name cannot exceed 100 characters")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(255, ErrorMessage = "Email cannot exceed 255 characters")]
        public string Email { get; set; } = string.Empty;

        [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Department is required")]
        [StringLength(100, ErrorMessage = "Department cannot exceed 100 characters")]
        public string Department { get; set; } = string.Empty;

        [Required(ErrorMessage = "Job title is required")]
        [StringLength(100, ErrorMessage = "Job title cannot exceed 100 characters")]
        public string JobTitle { get; set; } = string.Empty;

        [Required(ErrorMessage = "Employment status is required")]
        [StringLength(50, ErrorMessage = "Employment status cannot exceed 50 characters")]
        public string EmploymentStatus { get; set; } = "Full-time";

        public DateOnly? HireDate { get; set; }

        public DateOnly? TerminationDate { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Salary must be non-negative")]
        public decimal? Salary { get; set; }

        [StringLength(1000, ErrorMessage = "Skills cannot exceed 1000 characters")]
        public string? Skills { get; set; }

        [StringLength(500, ErrorMessage = "Certifications cannot exceed 500 characters")]
        public string? Certifications { get; set; }

        [StringLength(1000, ErrorMessage = "Experience cannot exceed 1000 characters")]
        public string? Experience { get; set; }

        [StringLength(500, ErrorMessage = "Education cannot exceed 500 characters")]
        public string? Education { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Tenant ID is required")]
        public Guid TenantId { get; set; }
    }

    public class UpdateResourceDto
    {
        [StringLength(100, ErrorMessage = "First name cannot exceed 100 characters")]
        public string? FirstName { get; set; }

        [StringLength(100, ErrorMessage = "Last name cannot exceed 100 characters")]
        public string? LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(255, ErrorMessage = "Email cannot exceed 255 characters")]
        public string? Email { get; set; }

        [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters")]
        public string? PhoneNumber { get; set; }

        [StringLength(100, ErrorMessage = "Department cannot exceed 100 characters")]
        public string? Department { get; set; }

        [StringLength(100, ErrorMessage = "Job title cannot exceed 100 characters")]
        public string? JobTitle { get; set; }

        [StringLength(50, ErrorMessage = "Employment status cannot exceed 50 characters")]
        public string? EmploymentStatus { get; set; }

        public DateOnly? HireDate { get; set; }

        public DateOnly? TerminationDate { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Salary must be non-negative")]
        public decimal? Salary { get; set; }

        [StringLength(1000, ErrorMessage = "Skills cannot exceed 1000 characters")]
        public string? Skills { get; set; }

        [StringLength(500, ErrorMessage = "Certifications cannot exceed 500 characters")]
        public string? Certifications { get; set; }

        [StringLength(1000, ErrorMessage = "Experience cannot exceed 1000 characters")]
        public string? Experience { get; set; }

        [StringLength(500, ErrorMessage = "Education cannot exceed 500 characters")]
        public string? Education { get; set; }
    }
} 