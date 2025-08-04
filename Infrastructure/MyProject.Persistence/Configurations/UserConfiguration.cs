using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyProject.Persistence.Entities;

namespace MyProject.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User", "ai");

            builder.HasKey(u => u.UserId);
            builder.Property(u => u.UserId)
                .HasColumnName("user_id")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(u => u.Email)
                .HasColumnName("email")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(u => u.PasswordHash)
                .HasColumnName("password_hash")
                .IsRequired();

            builder.Property(u => u.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.LastName)
                .HasColumnName("last_name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.IsActive)
                .HasColumnName("is_active")
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(u => u.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("now()");

            builder.Property(u => u.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("now()");

            // Unique constraint on email
            builder.HasIndex(u => u.Email)
                .IsUnique()
                .HasDatabaseName("user_email_key");

            // Indexes
            builder.HasIndex(u => u.Email)
                .HasDatabaseName("idx_user_email");

            builder.HasIndex(u => u.IsActive)
                .HasDatabaseName("idx_user_is_active");

            builder.HasIndex(u => u.FirstName)
                .HasDatabaseName("idx_user_first_name");

            builder.HasIndex(u => u.LastName)
                .HasDatabaseName("idx_user_last_name");

            // Relationships
            builder.HasMany(u => u.OrganizationUsers)
                .WithOne(ou => ou.User)
                .HasForeignKey(ou => ou.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
} 