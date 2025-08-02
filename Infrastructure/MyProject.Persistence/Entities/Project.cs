using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Persistence.Entities
{
    [Table("Project", Schema = "ai")]
    public class Project
    {
        [Key]
        [Column("project_id")]
        public Guid ProjectId { get; set; } = Guid.NewGuid();

        [Column("project_name")]
        [MaxLength(250)]
        public string? ProjectName { get; set; }

        [Column("project_type")]
        [MaxLength(100)]
        public string? ProjectType { get; set; }

        [Column("industry_domain")]
        [MaxLength(100)]
        public string? IndustryDomain { get; set; }

        [Column("planned_start_date")]
        public DateOnly? PlannedStartDate { get; set; }

        [Column("planned_end_date")]
        public DateOnly? PlannedEndDate { get; set; }

        [Column("actual_start_date")]
        public DateOnly? ActualStartDate { get; set; }

        [Column("actual_end_date")]
        public DateOnly? ActualEndDate { get; set; }

        [Column("planned_budget", TypeName = "numeric(18,2)")] 
        public decimal? PlannedBudget { get; set; }

        [Column("actual_budget", TypeName = "numeric(18,2)")] 
        public decimal? ActualBudget { get; set; }

        [Column("project_manager_id")]
        public Guid? ProjectManagerId { get; set; }

        [Column("team_size")]
        public int? TeamSize { get; set; }

        [Column("contract_type")]
        [MaxLength(50)]
        public string? ContractType { get; set; }

        [Column("project_status")]
        [MaxLength(50)]
        public string ProjectStatus { get; set; } = "planned";

        [Column("project_complexity")]
        [MaxLength(50)]
        public string? ProjectComplexity { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [Column("description")]
        public string? Description { get; set; }

        [Column("notes")]
        public string? Notes { get; set; }

        [Column("current_phase")]
        [MaxLength(100)]
        public string? CurrentPhase { get; set; }

        [Column("organization_id")]
        public Guid? OrganizationId { get; set; }
    }
} 