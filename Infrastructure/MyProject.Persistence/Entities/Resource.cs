using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Persistence.Entities
{
    [Table("Resource", Schema = "ai")]
    public class Resource
    {
        [Key]
        [Column("resource_id")]
        public Guid ResourceId { get; set; } = Guid.NewGuid();

        [Column("first_name")]
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Column("last_name")]
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Column("email")]
        [MaxLength(255)]
        public string? Email { get; set; }

        [Column("role_title")]
        [Required]
        [MaxLength(100)]
        public string RoleTitle { get; set; } = string.Empty;

        [Column("department")]
        [MaxLength(100)]
        public string? Department { get; set; }

        [Column("skills")]
        public string[]? Skills { get; set; }

        [Column("experience_level")]
        [MaxLength(50)]
        public string? ExperienceLevel { get; set; }

        [Column("full_time_equivalent", TypeName = "numeric(3,2)")] 
        [Range(0.0, 1.0)]
        public decimal FullTimeEquivalent { get; set; } = 1.0m;

        [Column("weekly_capacity_hours", TypeName = "numeric(5,2)")]
        
        [Range(0, double.MaxValue)]
        public decimal? WeeklyCapacityHours { get; set; }

        [Column("cost_rate_per_hour", TypeName = "numeric(10,2)")] 
        [Range(0, double.MaxValue)]
        public decimal? CostRatePerHour { get; set; }

        [Column("location")]
        [MaxLength(100)]
        public string? Location { get; set; }

        [Column("employment_status")]
        [Required]
        [MaxLength(50)]
        public string EmploymentStatus { get; set; } = "Active";

        [Column("start_date_employment")]
        public DateOnly? StartDateEmployment { get; set; }

        [Column("end_date_employment")]
        public DateOnly? EndDateEmployment { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [Column("user_id")]
        [Required]
        public Guid UserId { get; set; }

        [Column("tenant_id")]
        [Required]
        public Guid TenantId { get; set; }

        // Navigation properties
        public User? User { get; set; }
        public Organization? Tenant { get; set; }

        // Computed property for full name
        public string FullName => $"{FirstName} {LastName}".Trim();
    }
} 