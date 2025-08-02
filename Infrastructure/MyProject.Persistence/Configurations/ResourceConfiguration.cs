using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyProject.Persistence.Entities;

namespace MyProject.Persistence.Configurations
{
    public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.ToTable("Resource", "ai");

            builder.HasKey(r => r.ResourceId);
            builder.Property(r => r.ResourceId)
                .HasColumnName("resource_id")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(r => r.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(r => r.LastName)
                .HasColumnName("last_name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(r => r.Email)
                .HasColumnName("email")
                .HasMaxLength(255);

            builder.Property(r => r.RoleTitle)
                .HasColumnName("role_title")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(r => r.Department)
                .HasColumnName("department")
                .HasMaxLength(100);

            builder.Property(r => r.Skills)
                .HasColumnName("skills")
                .HasColumnType("text[]");

            builder.Property(r => r.ExperienceLevel)
                .HasColumnName("experience_level")
                .HasMaxLength(50);

            builder.Property(r => r.FullTimeEquivalent)
                .HasColumnName("full_time_equivalent")
                .HasColumnType("numeric(3,2)")
                .HasDefaultValue(1.0m);

            builder.Property(r => r.WeeklyCapacityHours)
                .HasColumnName("weekly_capacity_hours")
                .HasColumnType("numeric(5,2)");

            builder.Property(r => r.CostRatePerHour)
                .HasColumnName("cost_rate_per_hour")
                .HasColumnType("numeric(10,2)");

            builder.Property(r => r.Location)
                .HasColumnName("location")
                .HasMaxLength(100);

            builder.Property(r => r.EmploymentStatus)
                .HasColumnName("employment_status")
                .HasMaxLength(50)
                .HasDefaultValue("Active");

            builder.Property(r => r.StartDateEmployment)
                .HasColumnName("start_date_employment");

            builder.Property(r => r.EndDateEmployment)
                .HasColumnName("end_date_employment");

            builder.Property(r => r.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("now()");

            builder.Property(r => r.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("now()");

            // Unique constraint on email
            builder.HasIndex(r => r.Email)
                .IsUnique()
                .HasDatabaseName("resource_email_key");

            // Indexes
            builder.HasIndex(r => r.Department)
                .HasDatabaseName("idx_resources_department");

            builder.HasIndex(r => r.EmploymentStatus)
                .HasDatabaseName("idx_resources_employment_status");

            builder.HasIndex(r => r.LastName)
                .HasDatabaseName("idx_resources_last_name");

            builder.HasIndex(r => r.Location)
                .HasDatabaseName("idx_resources_location");

            builder.HasIndex(r => r.RoleTitle)
                .HasDatabaseName("idx_resources_role_title");

            builder.HasIndex(r => r.Skills)
                .HasDatabaseName("idx_resources_skills")
                .HasMethod("gin");

            // Check constraints (these will be handled by the database)
            // The actual constraints are defined in the PostgreSQL table
        }
    }
} 