using System.ComponentModel.DataAnnotations;

namespace MyProject.WebAPI.DTOs
{
    public class ResourceDto
    {
        public Guid ResourceId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string RoleTitle { get; set; } = string.Empty;
        public string? Department { get; set; }
        public string[]? Skills { get; set; }
        public string? ExperienceLevel { get; set; }
        public decimal FullTimeEquivalent { get; set; }
        public decimal? WeeklyCapacityHours { get; set; }
        public decimal? CostRatePerHour { get; set; }
        public string? Location { get; set; }
        public string EmploymentStatus { get; set; } = "Active";
        public DateOnly? StartDateEmployment { get; set; }
        public DateOnly? EndDateEmployment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public UserDto? User { get; set; }
        public OrganizationDto? Tenant { get; set; }
    }

    public class CreateResourceDto
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [EmailAddress]
        [MaxLength(255)]
        public string? Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string RoleTitle { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Department { get; set; }

        public string[]? Skills { get; set; }

        [MaxLength(50)]
        public string? ExperienceLevel { get; set; }

        [Range(0.0, 1.0)]
        public decimal FullTimeEquivalent { get; set; } = 1.0m;

        [Range(0, double.MaxValue)]
        public decimal? WeeklyCapacityHours { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? CostRatePerHour { get; set; }

        [MaxLength(100)]
        public string? Location { get; set; }

        [MaxLength(50)]
        public string EmploymentStatus { get; set; } = "Active";

        public DateOnly? StartDateEmployment { get; set; }

        public DateOnly? EndDateEmployment { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid TenantId { get; set; }
    }

    public class UpdateResourceDto
    {
        [MaxLength(100)]
        public string? FirstName { get; set; }

        [MaxLength(100)]
        public string? LastName { get; set; }

        [EmailAddress]
        [MaxLength(255)]
        public string? Email { get; set; }

        [MaxLength(100)]
        public string? RoleTitle { get; set; }

        [MaxLength(100)]
        public string? Department { get; set; }

        public string[]? Skills { get; set; }

        [MaxLength(50)]
        public string? ExperienceLevel { get; set; }

        [Range(0.0, 1.0)]
        public decimal? FullTimeEquivalent { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? WeeklyCapacityHours { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? CostRatePerHour { get; set; }

        [MaxLength(100)]
        public string? Location { get; set; }

        [MaxLength(50)]
        public string? EmploymentStatus { get; set; }

        public DateOnly? StartDateEmployment { get; set; }

        public DateOnly? EndDateEmployment { get; set; }
    }
} 