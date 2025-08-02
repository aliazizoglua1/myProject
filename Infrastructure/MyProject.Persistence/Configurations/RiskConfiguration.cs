using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyProject.Persistence.Entities;

namespace MyProject.Persistence.Configurations
{
    public class RiskConfiguration : IEntityTypeConfiguration<Risk>
    {
        public void Configure(EntityTypeBuilder<Risk> builder)
        {
            builder.ToTable("Risk", "ai");

            builder.HasKey(r => r.RiskId);
            builder.Property(r => r.RiskId)
                .HasColumnName("risk_id")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(r => r.ProjectId)
                .HasColumnName("project_id")
                .IsRequired();

            builder.Property(r => r.RiskDescription)
                .HasColumnName("risk_description")
                .IsRequired();

            builder.Property(r => r.RiskCategory)
                .HasColumnName("risk_category")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(r => r.PlannedProbabilityScore)
                .HasColumnName("planned_probability_score");

            builder.Property(r => r.ActualProbabilityScore)
                .HasColumnName("actual_probability_score");

            builder.Property(r => r.PlannedImpactScore)
                .HasColumnName("planned_impact_score");

            builder.Property(r => r.ActualImpactScore)
                .HasColumnName("actual_impact_score");

            builder.Property(r => r.PlannedRiskExposure)
                .HasColumnName("planned_risk_exposure");

            builder.Property(r => r.ActualRiskExposure)
                .HasColumnName("actual_risk_exposure");

            builder.Property(r => r.MitigationStrategy)
                .HasColumnName("mitigation_strategy");

            builder.Property(r => r.ContingencyPlan)
                .HasColumnName("contingency_plan");

            builder.Property(r => r.TriggerEvents)
                .HasColumnName("trigger_events");

            builder.Property(r => r.RiskOwnerId)
                .HasColumnName("risk_owner_id");

            builder.Property(r => r.RiskStatus)
                .HasColumnName("risk_status")
                .HasMaxLength(50)
                .HasDefaultValue("Open");

            builder.Property(r => r.IdentifiedDate)
                .HasColumnName("identified_date")
                .HasDefaultValueSql("CURRENT_DATE");

            builder.Property(r => r.LastReviewDate)
                .HasColumnName("last_review_date");

            builder.Property(r => r.ClosureDate)
                .HasColumnName("closure_date");

            builder.Property(r => r.OutcomeDescription)
                .HasColumnName("outcome_description");

            builder.Property(r => r.ActualImpactOnScheduleDays)
                .HasColumnName("actual_impact_on_schedule_days");

            builder.Property(r => r.ActualImpactOnBudgetUsd)
                .HasColumnName("actual_impact_on_budget_usd")
                .HasColumnType("numeric(18,2)");

            builder.Property(r => r.LessonsLearned)
                .HasColumnName("lessons_learned");

            builder.Property(r => r.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("now()");

            builder.Property(r => r.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("now()");

            // Foreign key relationship
            builder.HasOne(r => r.Project)
                .WithMany()
                .HasForeignKey(r => r.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(r => r.RiskCategory)
                .HasDatabaseName("idx_risks_category");

            builder.HasIndex(r => r.IdentifiedDate)
                .HasDatabaseName("idx_risks_identified_date");

            builder.HasIndex(r => r.RiskOwnerId)
                .HasDatabaseName("idx_risks_owner_id");

            builder.HasIndex(r => r.ProjectId)
                .HasDatabaseName("idx_risks_project_id");

            builder.HasIndex(r => r.RiskStatus)
                .HasDatabaseName("idx_risks_status");

            // Check constraints (these will be handled by the database)
            // The actual constraints are defined in the PostgreSQL table
        }
    }
} 