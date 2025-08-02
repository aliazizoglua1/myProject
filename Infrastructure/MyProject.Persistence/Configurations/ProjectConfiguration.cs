using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyProject.Persistence.Entities;

namespace MyProject.Persistence.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Project", "ai");

            builder.HasKey(p => p.ProjectId);
            builder.Property(p => p.ProjectId)
                .HasColumnName("project_id")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(p => p.ProjectName)
                .HasColumnName("project_name")
                .HasMaxLength(250);

            builder.Property(p => p.ProjectType)
                .HasColumnName("project_type")
                .HasMaxLength(100);

            builder.Property(p => p.IndustryDomain)
                .HasColumnName("industry_domain")
                .HasMaxLength(100);

            builder.Property(p => p.PlannedStartDate)
                .HasColumnName("planned_start_date");

            builder.Property(p => p.PlannedEndDate)
                .HasColumnName("planned_end_date");

            builder.Property(p => p.ActualStartDate)
                .HasColumnName("actual_start_date");

            builder.Property(p => p.ActualEndDate)
                .HasColumnName("actual_end_date");

            builder.Property(p => p.PlannedBudget)
                .HasColumnName("planned_budget")
                .HasColumnType("numeric(18,2)");

            builder.Property(p => p.ActualBudget)
                .HasColumnName("actual_budget")
                .HasColumnType("numeric(18,2)");

            builder.Property(p => p.ProjectManagerId)
                .HasColumnName("project_manager_id");

            builder.Property(p => p.TeamSize)
                .HasColumnName("team_size");

            builder.Property(p => p.ContractType)
                .HasColumnName("contract_type")
                .HasMaxLength(50);

            builder.Property(p => p.ProjectStatus)
                .HasColumnName("project_status")
                .HasMaxLength(50)
                .HasDefaultValue("planned");

            builder.Property(p => p.ProjectComplexity)
                .HasColumnName("project_complexity")
                .HasMaxLength(50);

            builder.Property(p => p.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("now()");

            builder.Property(p => p.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("now()");

            builder.Property(p => p.Description)
                .HasColumnName("description");

            builder.Property(p => p.Notes)
                .HasColumnName("notes");

            builder.Property(p => p.CurrentPhase)
                .HasColumnName("current_phase")
                .HasMaxLength(100);

            builder.Property(p => p.OrganizationId)
                .HasColumnName("organization_id");

            // Indexes
            builder.HasIndex(p => p.PlannedStartDate)
                .HasDatabaseName("idx_planned_start_date");

            builder.HasIndex(p => p.ProjectManagerId)
                .HasDatabaseName("idx_project_manager_id");

            builder.HasIndex(p => p.ProjectName)
                .HasDatabaseName("idx_project_name");

            builder.HasIndex(p => p.ProjectStatus)
                .HasDatabaseName("idx_project_status");

            // Check constraints (these will be handled by the database)
            // The actual constraints are defined in the PostgreSQL table
        }
    }
} 