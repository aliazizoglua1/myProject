using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Persistence.Entities;

public class TaskHistory
{
    [Key] 
    [Column("history_id")]
    public Guid TaskHistoryId { get; set; }
    
    [Required] 
    [Column("task_id")]
    public Guid TaskId { get; set; }
    
   // [Required]
    [Column("user_id")]
    public Guid UserId { get; set; }
    
    //[Required]
    [Column(TypeName = "text")]
    public string Comment { get; set; } = string.Empty;
    
    [Required]
    [Column("created_at", TypeName = "timestamp with time zone")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
    
    [Required]
    [Column("organization_id")]
    public Guid OrganizationId { get; set; }
    
    // Navigation properties
    public Task Task { get; set; } = null!;
    public User User { get; set; } = null!;
    public Organization Organization { get; set; } = null!;
} 