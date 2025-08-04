using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Persistence.Entities
{
    [Table("OrganizationUser", Schema = "ai")]
    public class OrganizationUser
    {
        [Column("organization_id")]
        public Guid OrganizationId { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("role")]
        [Required]
        [MaxLength(50)]
        public string Role { get; set; } = string.Empty;

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Organization? Organization { get; set; }
        public User? User { get; set; }
    }
} 