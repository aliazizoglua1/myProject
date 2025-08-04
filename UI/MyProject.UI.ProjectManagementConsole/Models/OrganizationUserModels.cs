using System.ComponentModel.DataAnnotations;

namespace MyProject.UI.ProjectManagementConsole.Models
{
    public class OrganizationUserDto
    {
        public Guid OrganizationId { get; set; }
        public Guid UserId { get; set; }
        public string Role { get; set; } = string.Empty;
        public string AccessLevel { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime JoinedDate { get; set; }
        public DateTime? LastAccessDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public OrganizationDto? Organization { get; set; }
        public UserDto? User { get; set; }
    }

    public class CreateOrganizationUserDto
    {
        [Required(ErrorMessage = "Organization ID is required")]
        public Guid OrganizationId { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [StringLength(50, ErrorMessage = "Role cannot exceed 50 characters")]
        public string Role { get; set; } = string.Empty;

        [Required(ErrorMessage = "Access level is required")]
        [StringLength(50, ErrorMessage = "Access level cannot exceed 50 characters")]
        public string AccessLevel { get; set; } = "Member";

        public bool IsActive { get; set; } = true;

        public DateOnly? JoinedDate { get; set; }
    }

    public class UpdateOrganizationUserDto
    {
        [StringLength(50, ErrorMessage = "Role cannot exceed 50 characters")]
        public string? Role { get; set; }

        [StringLength(50, ErrorMessage = "Access level cannot exceed 50 characters")]
        public string? AccessLevel { get; set; }

        public bool? IsActive { get; set; }

        public DateOnly? JoinedDate { get; set; }

        public DateTime? LastAccessDate { get; set; }
    }

    public class OrganizationUserWithDetailsDto
    {
        public Guid OrganizationId { get; set; }
        public Guid UserId { get; set; }
        public string Role { get; set; } = string.Empty;
        public string AccessLevel { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime JoinedDate { get; set; }
        public DateTime? LastAccessDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public OrganizationDto Organization { get; set; } = new();
        public UserDto User { get; set; } = new();
    }
} 