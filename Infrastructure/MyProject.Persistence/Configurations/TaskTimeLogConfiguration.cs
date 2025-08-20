using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyProject.Persistence.Entities;

namespace MyProject.Persistence.Configurations
{
    public class TaskTimeLogConfiguration : IEntityTypeConfiguration<TaskTimeLog>
    {
        public void Configure(EntityTypeBuilder<TaskTimeLog> builder)
        {
            builder.ToTable("TaskTimeLog", "ai");

            builder.HasKey(t => t.TaskTimeLogId);
            builder.Property(t => t.TaskTimeLogId)
                .HasColumnName("task_time_log_id")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(t => t.TaskId)
                .HasColumnName("task_id")
                .IsRequired();

            builder.Property(t => t.ResourceId)
                .HasColumnName("resource_id")
                .IsRequired();

            builder.Property(t => t.DateWorked)
                .HasColumnName("date_worked")
                .IsRequired();

            builder.Property(t => t.HoursWorked)
                .HasColumnName("hours_worked")
                .HasColumnType("numeric(8,2)")
                .IsRequired();

            builder.Property(t => t.Description)
                .HasColumnName("description")
                .HasMaxLength(1000);

            builder.Property(t => t.WorkType)
                .HasColumnName("work_type")
                .HasMaxLength(100);

            builder.Property(t => t.IsBillable)
                .HasColumnName("is_billable")
                .HasDefaultValue(true);

            builder.Property(t => t.HourlyRate)
                .HasColumnName("hourly_rate")
                .HasColumnType("numeric(10,2)");

            builder.Property(t => t.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("now()");

            builder.Property(t => t.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("now()");

            builder.Property(t => t.OrganizationId)
                .HasColumnName("organization_id")
                .IsRequired();

            // Foreign key relationships
            builder.HasOne(t => t.Task)
                .WithMany(task => task.TaskTimeLogs)
                .HasForeignKey(t => t.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Resource)
                .WithMany(r => r.TaskTimeLogs)
                .HasForeignKey(t => t.ResourceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Organization)
                .WithMany(o => o.TaskTimeLogs)
                .HasForeignKey(t => t.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes for better performance
            builder.HasIndex(t => t.TaskId);
            builder.HasIndex(t => t.ResourceId);
            builder.HasIndex(t => t.DateWorked);
            builder.HasIndex(t => t.OrganizationId);
        }
    }
} 