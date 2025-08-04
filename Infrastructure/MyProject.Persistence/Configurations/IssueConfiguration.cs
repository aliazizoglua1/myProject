using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyProject.Persistence.Entities;

namespace MyProject.Persistence.Configurations
{
    public class IssueConfiguration : IEntityTypeConfiguration<Issue>
    {
        public void Configure(EntityTypeBuilder<Issue> builder)
        {
            builder.ToTable("Issue", "ai");

            builder.HasKey(i => i.IssueId);
            builder.Property(i => i.IssueId)
                .HasColumnName("issue_id")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(i => i.ProjectId)
                .HasColumnName("project_id")
                .IsRequired();

            builder.Property(i => i.TaskId)
                .HasColumnName("task_id");

            builder.Property(i => i.IssueName)
                .HasColumnName("issue_name")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(i => i.IssueDescription)
                .HasColumnName("issue_description")
                .IsRequired();

            builder.Property(i => i.IssueType)
                .HasColumnName("issue_type")
                .HasMaxLength(100);

            builder.Property(i => i.RootCause)
                .HasColumnName("root_cause");

            builder.Property(i => i.ImpactOnScheduleDays)
                .HasColumnName("impact_on_schedule_days");

            builder.Property(i => i.ImpactOnBudgetUsd)
                .HasColumnName("impact_on_budget_usd")
                .HasColumnType("numeric(18,2)");

            builder.Property(i => i.ImpactOnScope)
                .HasColumnName("impact_on_scope");

            builder.Property(i => i.ImpactOnQuality)
                .HasColumnName("impact_on_quality");

            builder.Property(i => i.ResolutionSteps)
                .HasColumnName("resolution_steps");

            builder.Property(i => i.Severity)
                .HasColumnName("severity")
                .HasMaxLength(50)
                .HasDefaultValue("Medium");

            builder.Property(i => i.Priority)
                .HasColumnName("priority")
                .HasMaxLength(50)
                .HasDefaultValue("Normal");

            builder.Property(i => i.AssignedToUserId)
                .HasColumnName("assigned_to_user_id");

            builder.Property(i => i.Status)
                .HasColumnName("status")
                .HasMaxLength(50)
                .HasDefaultValue("Open");

            builder.Property(i => i.OpenedDate)
                .HasColumnName("opened_date")
                .HasDefaultValueSql("CURRENT_DATE");

            builder.Property(i => i.ClosedDate)
                .HasColumnName("closed_date");

            builder.Property(i => i.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("now()");

            builder.Property(i => i.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("now()");

            builder.Property(i => i.TenantId)
                .HasColumnName("tenant_id")
                .IsRequired();

            builder.Property(i => i.MilestoneId)
                .HasColumnName("milestone_id");

            // Foreign key relationships
            builder.HasOne(i => i.Project)
                .WithMany()
                .HasForeignKey(i => i.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.Task)
                .WithMany()
                .HasForeignKey(i => i.TaskId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(i => i.AssignedToUser)
                .WithMany()
                .HasForeignKey(i => i.AssignedToUserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(i => i.Tenant)
                .WithMany()
                .HasForeignKey(i => i.TenantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.Milestone)
                .WithMany()
                .HasForeignKey(i => i.MilestoneId)
                .OnDelete(DeleteBehavior.SetNull);

            // Indexes
            builder.HasIndex(i => i.AssignedToUserId)
                .HasDatabaseName("idx_issues_assigned_to_user_id");

            builder.HasIndex(i => i.OpenedDate)
                .HasDatabaseName("idx_issues_opened_date");

            builder.HasIndex(i => i.Priority)
                .HasDatabaseName("idx_issues_priority");

            builder.HasIndex(i => i.ProjectId)
                .HasDatabaseName("idx_issues_project_id");

            builder.HasIndex(i => i.Severity)
                .HasDatabaseName("idx_issues_severity");

            builder.HasIndex(i => i.Status)
                .HasDatabaseName("idx_issues_status");

            builder.HasIndex(i => i.TaskId)
                .HasDatabaseName("idx_issues_task_id");

            builder.HasIndex(i => i.TenantId)
                .HasDatabaseName("idx_issues_tenant_id");

            builder.HasIndex(i => i.MilestoneId)
                .HasDatabaseName("idx_issues_milestone_id");

            // Check constraints (these will be handled by the database)
            // The actual constraints are defined in the PostgreSQL table
        }
    }
} 