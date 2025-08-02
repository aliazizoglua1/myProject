using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyProject.Persistence.Entities;

namespace MyProject.Persistence.Configurations
{
    public class QualityAssuranceConfiguration : IEntityTypeConfiguration<QualityAssurance>
    {
        public void Configure(EntityTypeBuilder<QualityAssurance> builder)
        {
            builder.ToTable("QualityAssurance", "ai");

            builder.HasKey(qa => qa.QaItemId);
            builder.Property(qa => qa.QaItemId)
                .HasColumnName("qa_item_id")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(qa => qa.ProjectId)
                .HasColumnName("project_id")
                .IsRequired();

            builder.Property(qa => qa.TaskId)
                .HasColumnName("task_id");

            builder.Property(qa => qa.ItemType)
                .HasColumnName("item_type")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(qa => qa.Summary)
                .HasColumnName("summary")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(qa => qa.Description)
                .HasColumnName("description")
                .IsRequired();

            builder.Property(qa => qa.Severity)
                .HasColumnName("severity")
                .HasMaxLength(50);

            builder.Property(qa => qa.Priority)
                .HasColumnName("priority")
                .HasMaxLength(50);

            builder.Property(qa => qa.Status)
                .HasColumnName("status")
                .HasMaxLength(50)
                .HasDefaultValue("Open");

            builder.Property(qa => qa.ReportedByUserId)
                .HasColumnName("reported_by_user_id");

            builder.Property(qa => qa.AssignedToUserId)
                .HasColumnName("assigned_to_user_id");

            builder.Property(qa => qa.ReportedDate)
                .HasColumnName("reported_date")
                .HasDefaultValueSql("CURRENT_DATE");

            builder.Property(qa => qa.ResolutionDate)
                .HasColumnName("resolution_date");

            builder.Property(qa => qa.ClosedDate)
                .HasColumnName("closed_date");

            builder.Property(qa => qa.ResolutionNotes)
                .HasColumnName("resolution_notes");

            builder.Property(qa => qa.TestEnvironment)
                .HasColumnName("test_environment");

            builder.Property(qa => qa.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("now()");

            builder.Property(qa => qa.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("now()");

            // Foreign key relationships
            builder.HasOne(qa => qa.Project)
                .WithMany()
                .HasForeignKey(qa => qa.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(qa => qa.Task)
                .WithMany()
                .HasForeignKey(qa => qa.TaskId)
                .OnDelete(DeleteBehavior.SetNull);

            // Indexes
            builder.HasIndex(qa => qa.ItemType)
                .HasDatabaseName("idx_qa_item_type");

            builder.HasIndex(qa => qa.Priority)
                .HasDatabaseName("idx_qa_priority");

            builder.HasIndex(qa => qa.ProjectId)
                .HasDatabaseName("idx_qa_project_id");

            builder.HasIndex(qa => qa.Severity)
                .HasDatabaseName("idx_qa_severity");

            builder.HasIndex(qa => qa.Status)
                .HasDatabaseName("idx_qa_status");

            builder.HasIndex(qa => qa.TaskId)
                .HasDatabaseName("idx_qa_task_id");

            // Check constraints (these will be handled by the database)
            // The actual constraints are defined in the PostgreSQL table
        }
    }
} 