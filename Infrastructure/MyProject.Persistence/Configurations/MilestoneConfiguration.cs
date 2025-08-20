using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyProject.Persistence.Entities;

namespace MyProject.Persistence.Configurations
{
    public class MilestoneConfiguration : IEntityTypeConfiguration<Milestone>
    {
        public void Configure(EntityTypeBuilder<Milestone> builder)
        {
            builder.ToTable("Milestone", "ai");

            builder.HasKey(m => m.MilestoneId);
            builder.Property(m => m.MilestoneId)
                .HasColumnName("milestone_id")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(m => m.ProjectId)
                .HasColumnName("project_id")
                .IsRequired();

            builder.Property(m => m.MilestoneName)
                .HasColumnName("milestone_name")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(m => m.Description)
                .HasColumnName("description");

            builder.Property(m => m.DueDate)
                .HasColumnName("due_date")
                .IsRequired();

            builder.Property(m => m.IsAchieved)
                .HasColumnName("is_achieved")
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(m => m.CompletionDate)
                .HasColumnName("completion_date");

            builder.Property(m => m.OrganizationId)
                .HasColumnName("organization_id")
                .IsRequired();

            builder.Property(m => m.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("now()");

            builder.Property(m => m.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("now()");

            // Unique constraint on project_id and milestone_name
            builder.HasIndex(m => new { m.ProjectId, m.MilestoneName })
                .IsUnique()
                .HasDatabaseName("uq_milestone_project_name");

            // Relationships
            builder.HasOne(m => m.Project)
                .WithMany(p => p.Milestones)
                .HasForeignKey(m => m.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(m => m.Organization)
                .WithMany(o => o.Milestones)
                .HasForeignKey(m => m.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(m => m.ProjectId)
                .HasDatabaseName("idx_milestone_project_id");

            builder.HasIndex(m => m.OrganizationId)
                .HasDatabaseName("idx_milestone_organization_id");

            builder.HasIndex(m => m.DueDate)
                .HasDatabaseName("idx_milestone_due_date");

            builder.HasIndex(m => m.IsAchieved)
                .HasDatabaseName("idx_milestone_is_achieved");

            builder.HasIndex(m => m.MilestoneName)
                .HasDatabaseName("idx_milestone_name");
        }
    }
} 