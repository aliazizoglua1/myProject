using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyProject.Persistence.Entities;

namespace MyProject.Persistence.Configurations
{
    public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.ToTable("Organization", "ai");

            builder.HasKey(o => o.OrganizationId);
            builder.Property(o => o.OrganizationId)
                .HasColumnName("organization_id")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(o => o.OrganizationName)
                .HasColumnName("organization_name")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(o => o.Status)
                .HasColumnName("status")
                .HasMaxLength(50)
                .IsRequired()
                .HasDefaultValue("Active");

            builder.Property(o => o.BillingPlan)
                .HasColumnName("billing_plan")
                .HasMaxLength(50);

            builder.Property(o => o.ContactEmail)
                .HasColumnName("contact_email")
                .HasMaxLength(255);

            builder.Property(o => o.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("now()");

            builder.Property(o => o.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("now()");

            // Unique constraint on organization_name
            builder.HasIndex(o => o.OrganizationName)
                .IsUnique()
                .HasDatabaseName("organization_organization_name_key");

            // Indexes
            builder.HasIndex(o => o.Status)
                .HasDatabaseName("idx_organization_status");

            builder.HasIndex(o => o.OrganizationName)
                .HasDatabaseName("idx_organization_name");

            // Check constraint for status values (handled by database)
            // The actual constraint is defined in the PostgreSQL table

            // Relationships
            builder.HasMany(o => o.Milestones)
                .WithOne(m => m.Tenant)
                .HasForeignKey(m => m.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(o => o.OrganizationUsers)
                .WithOne(ou => ou.Organization)
                .HasForeignKey(ou => ou.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
} 