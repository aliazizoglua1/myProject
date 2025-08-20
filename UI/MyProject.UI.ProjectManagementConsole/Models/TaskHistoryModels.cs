using System.ComponentModel.DataAnnotations;

namespace MyProject.UI.ProjectManagementConsole.Models;

public class TaskHistoryDto
{
    public Guid TaskHistoryId { get; set; }
    public Guid TaskId { get; set; }
    public Guid UserId { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid OrganizationId { get; set; }
    
    // Navigation properties
    public TaskDto? Task { get; set; }
    public UserDto? User { get; set; }
    public OrganizationDto? Organization { get; set; }
}

public class CreateTaskHistoryDto
{
    [Required]
    public Guid TaskId { get; set; }
    
    [Required]
    public Guid UserId { get; set; }

    [Required]
    //[StringLength(30000, ErrorMessage = "Comment cannot exceed 30000 characters")]
    public string Comment { get; set; } = string.Empty;

    [Required]
    public Guid OrganizationId { get; set; }
}

public class UpdateTaskHistoryDto
{
    [Required]
    [StringLength(10000, ErrorMessage = "Comment cannot exceed 10000 characters")]
    public string Comment { get; set; } = string.Empty;
} 