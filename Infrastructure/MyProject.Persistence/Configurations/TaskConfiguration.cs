using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyProject.Persistence.Entities;
using Task = MyProject.Persistence.Entities.Task;
namespace MyProject.Persistence.Configurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.ToTable("Task", "ai");

            builder.HasKey(t => t.TaskId);
            builder.Property(t => t.TaskId)
                .HasColumnName("task_id")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(t => t.ProjectId)
                .HasColumnName("project_id")
                .IsRequired();

            builder.Property(t => t.TaskName)
                .HasColumnName("task_name")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(t => t.TaskType)
                .HasColumnName("task_type")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.AssignedToUserId)
                .HasColumnName("assigned_to_user_id");

            builder.Property(t => t.ParentTaskId)
                .HasColumnName("parent_task_id");

            builder.Property(t => t.PlannedEffortHours)
                .HasColumnName("planned_effort_hours")
                .HasColumnType("numeric(8,2)");

            builder.Property(t => t.ActualEffortHours)
                .HasColumnName("actual_effort_hours")
                .HasColumnType("numeric(8,2)");

            builder.Property(t => t.PlannedStartDate)
                .HasColumnName("planned_start_date")
                .IsRequired();

            builder.Property(t => t.PlannedEndDate)
                .HasColumnName("planned_end_date");

            builder.Property(t => t.ActualStartDate)
                .HasColumnName("actual_start_date");

            builder.Property(t => t.ActualEndDate)
                .HasColumnName("actual_end_date");

            builder.Property(t => t.TaskStatus)
                .HasColumnName("task_status")
                .HasMaxLength(50)
                .HasDefaultValue("To Do");

            builder.Property(t => t.Priority)
                .HasColumnName("priority")
                .HasMaxLength(50)
                .HasDefaultValue("Medium");

            builder.Property(t => t.IsMilestone)
                .HasColumnName("is_milestone")
                .HasDefaultValue(false);

            builder.Property(t => t.Description)
                .HasColumnName("description");

            builder.Property(t => t.Comments)
                .HasColumnName("comments");

            builder.Property(t => t.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("now()");

            builder.Property(t => t.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("now()");

            // Foreign key relationships
            builder.HasOne(t => t.Project)
                .WithMany()
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.ParentTask)
                .WithMany(t => t.Subtasks)
                .HasForeignKey(t => t.ParentTaskId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(t => t.AssignedToUserId)
                .HasDatabaseName("idx_tasks_assigned_to_user_id");

            builder.HasIndex(t => t.ParentTaskId)
                .HasDatabaseName("idx_tasks_parent_task_id");

            builder.HasIndex(t => t.PlannedStartDate)
                .HasDatabaseName("idx_tasks_planned_start_date");

            builder.HasIndex(t => t.Priority)
                .HasDatabaseName("idx_tasks_priority");

            builder.HasIndex(t => t.ProjectId)
                .HasDatabaseName("idx_tasks_project_id");

            builder.HasIndex(t => t.TaskStatus)
                .HasDatabaseName("idx_tasks_status");

            // Check constraints (these will be handled by the database)
            // The actual constraints are defined in the PostgreSQL table
        }
    }
} 