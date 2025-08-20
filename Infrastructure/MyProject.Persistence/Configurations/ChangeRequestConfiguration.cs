using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyProject.Persistence.Entities;

namespace MyProject.Persistence.Configurations
{
    public class ChangeRequestConfiguration : IEntityTypeConfiguration<ChangeRequest>
    {
        public void Configure(EntityTypeBuilder<ChangeRequest> builder)
        {
            builder.ToTable("ChangeRequest", "ai");

            builder.HasKey(cr => cr.ChangeRequestId);
            builder.Property(cr => cr.ChangeRequestId)
                .HasColumnName("change_request_id")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(cr => cr.ProjectId)
                .HasColumnName("project_id")
                .IsRequired();

            builder.Property(cr => cr.RequestTitle)
                .HasColumnName("request_title")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(cr => cr.RequestDescription)
                .HasColumnName("request_description")
                .IsRequired();

            builder.Property(cr => cr.ReasonForChange)
                .HasColumnName("reason_for_change")
                .IsRequired();

            builder.Property(cr => cr.EstimatedImpactOnScope)
                .HasColumnName("estimated_impact_on_scope");

            builder.Property(cr => cr.EstimatedImpactOnScheduleDays)
                .HasColumnName("estimated_impact_on_schedule_days");

            builder.Property(cr => cr.EstimatedImpactOnBudgetUsd)
                .HasColumnName("estimated_impact_on_budget_usd")
                .HasColumnType("numeric(18,2)");

            builder.Property(cr => cr.EstimatedImpactOnQuality)
                .HasColumnName("estimated_impact_on_quality");

            builder.Property(cr => cr.RequestedByUserId)
                .HasColumnName("requested_by_user_id")
                .IsRequired();

            builder.Property(cr => cr.RequestDate)
                .HasColumnName("request_date")
                .HasDefaultValueSql("CURRENT_DATE");

            builder.Property(cr => cr.ApprovalStatus)
                .HasColumnName("approval_status")
                .HasMaxLength(50)
                .HasDefaultValue("Pending");

            builder.Property(cr => cr.ApprovedByUserId)
                .HasColumnName("approved_by_user_id");

            builder.Property(cr => cr.ApprovalDate)
                .HasColumnName("approval_date");

            builder.Property(cr => cr.ApprovalNotes)
                .HasColumnName("approval_notes");

            builder.Property(cr => cr.VersionAffected)
                .HasColumnName("version_affected")
                .HasMaxLength(50);

            builder.Property(cr => cr.ActualImpactOnScheduleDays)
                .HasColumnName("actual_impact_on_schedule_days");

            builder.Property(cr => cr.ActualImpactOnBudgetUsd)
                .HasColumnName("actual_impact_on_budget_usd")
                .HasColumnType("numeric(18,2)");

            builder.Property(cr => cr.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("now()");

            builder.Property(cr => cr.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("now()");

            builder.Property(cr => cr.OrganizationId)
                .HasColumnName("organization_id")
                .IsRequired();

            builder.Property(cr => cr.MilestoneId)
                .HasColumnName("milestone_id");

            // Foreign key relationships
            builder.HasOne(cr => cr.Project)
                .WithMany(p => p.ChangeRequests)
                .HasForeignKey(cr => cr.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cr => cr.RequestedByUser)
                .WithMany(u => u.RequestedChangeRequests)
                .HasForeignKey(cr => cr.RequestedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(cr => cr.ApprovedByUser)
                .WithMany(u => u.ApprovedChangeRequests)
                .HasForeignKey(cr => cr.ApprovedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(cr => cr.Organization)
                .WithMany(o => o.ChangeRequests)
                .HasForeignKey(cr => cr.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cr => cr.Milestone)
                .WithMany(m => m.ChangeRequests)
                .HasForeignKey(cr => cr.MilestoneId)
                .OnDelete(DeleteBehavior.SetNull);

            // Indexes
            builder.HasIndex(cr => cr.ProjectId)
                .HasDatabaseName("idx_change_requests_project_id");

            builder.HasIndex(cr => cr.RequestDate)
                .HasDatabaseName("idx_change_requests_request_date");

            builder.HasIndex(cr => cr.RequestedByUserId)
                .HasDatabaseName("idx_change_requests_requestor_id");

            builder.HasIndex(cr => cr.ApprovalStatus)
                .HasDatabaseName("idx_change_requests_status");

            builder.HasIndex(cr => cr.OrganizationId)
                .HasDatabaseName("idx_change_requests_organization_id");

            builder.HasIndex(cr => cr.MilestoneId)
                .HasDatabaseName("idx_change_requests_milestone_id");

            builder.HasIndex(cr => cr.ApprovedByUserId)
                .HasDatabaseName("idx_change_requests_approver_id");

            // Check constraints (these will be handled by the database)
            // The actual constraints are defined in the PostgreSQL table
        }
    }
} 