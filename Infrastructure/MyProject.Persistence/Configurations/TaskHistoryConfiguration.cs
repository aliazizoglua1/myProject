using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyProject.Persistence.Entities;

namespace MyProject.Persistence.Configurations;

public class TaskHistoryConfiguration : IEntityTypeConfiguration<TaskHistory>
{
    public void Configure(EntityTypeBuilder<TaskHistory> builder)
    {
        builder.ToTable("TaskHistory");
        
        builder.HasKey(th => th.TaskHistoryId);
        
        builder.Property(th => th.TaskHistoryId)
               .HasColumnName("history_id");
        
        builder.Property(th => th.TaskId)
               .HasColumnName("task_id")
               .IsRequired();
        
        //builder.Property(th => th.UserId)
        //       .HasColumnName("user_id")
        //       .IsRequired();
        
        builder.Property(th => th.Comment)
               .HasColumnName("comment")
               .HasColumnType("text")
               .IsRequired();
        
        builder.Property(th => th.CreatedAt)
               .HasColumnName("created_at")
               .HasColumnType("timestamp with time zone")
               .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        //builder.Property(th => th.UpdatedAt)
        //       .HasColumnName("updated_at")
        //       .HasColumnType("timestamp with time zone")
        //       .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        builder.Property(th => th.OrganizationId)
               .HasColumnName("organization_id")
               .IsRequired();
        
        // Foreign key relationships
        builder.HasOne(th => th.Task)
               .WithMany()
               .HasForeignKey(th => th.TaskId)
               .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(th => th.User)
               .WithMany()
               .HasForeignKey(th => th.UserId)
               .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(th => th.Organization)
               .WithMany()
               .HasForeignKey(th => th.OrganizationId)
               .OnDelete(DeleteBehavior.Restrict);
        
        // Indexes
        builder.HasIndex(th => th.TaskId);
        builder.HasIndex(th => th.UserId);
        builder.HasIndex(th => th.CreatedAt);
        builder.HasIndex(th => th.OrganizationId);
    }
} 