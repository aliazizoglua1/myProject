using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyProject.Persistence.Entities;

namespace MyProject.Persistence.Configurations
{
    public class OrganizationUserConfiguration : IEntityTypeConfiguration<OrganizationUser>
    {
        public void Configure(EntityTypeBuilder<OrganizationUser> builder)
        {
            builder.ToTable("OrganizationUser", "ai");

            // Composite primary key
            builder.HasKey(ou => new { ou.OrganizationId, ou.UserId });

            builder.Property(ou => ou.OrganizationId)
                .HasColumnName("organization_id");

            builder.Property(ou => ou.UserId)
                .HasColumnName("user_id");

            builder.Property(ou => ou.Role)
                .HasColumnName("role")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(ou => ou.IsActive)
                .HasColumnName("is_active")
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(ou => ou.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("now()");

            builder.Property(ou => ou.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("now()");

            // Relationships
            builder.HasOne(ou => ou.Organization)
                .WithMany()
                .HasForeignKey(ou => ou.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ou => ou.User)
                .WithMany()
                .HasForeignKey(ou => ou.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(ou => ou.OrganizationId)
                .HasDatabaseName("idx_organizationuser_organization_id");

            builder.HasIndex(ou => ou.UserId)
                .HasDatabaseName("idx_organizationuser_user_id");

            builder.HasIndex(ou => ou.Role)
                .HasDatabaseName("idx_organizationuser_role");

            builder.HasIndex(ou => ou.IsActive)
                .HasDatabaseName("idx_organizationuser_is_active");

            builder.HasIndex(ou => new { ou.OrganizationId, ou.IsActive })
                .HasDatabaseName("idx_organizationuser_org_active");

            builder.HasIndex(ou => new { ou.UserId, ou.IsActive })
                .HasDatabaseName("idx_organizationuser_user_active");
        }
    }
} 