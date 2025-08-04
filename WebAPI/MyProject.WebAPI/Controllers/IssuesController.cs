using Microsoft.AspNetCore.Mvc;
using MyProject.Persistence.Entities;
using MyProject.Persistence.Repositories;
using MyProject.WebAPI.DTOs;
using Task = MyProject.Persistence.Entities.Task;
namespace MyProject.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IssuesController : ControllerBase
    {
        private readonly IIssueRepository _issueRepository;

        public IssuesController(IIssueRepository issueRepository)
        {
            _issueRepository = issueRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IssueDto>>> GetIssues()
        {
            var issues = await _issueRepository.GetAllAsync();
            var issueDtos = issues.Select(MapToDto);
            return Ok(issueDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IssueDto>> GetIssue(Guid id)
        {
            var issue = await _issueRepository.GetByIdAsync(id);
            if (issue == null)
            {
                return NotFound();
            }
            return Ok(MapToDto(issue));
        }

        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<IEnumerable<IssueDto>>> GetIssuesByProject(Guid projectId)
        {
            var issues = await _issueRepository.GetByProjectIdAsync(projectId);
            var issueDtos = issues.Select(MapToDto);
            return Ok(issueDtos);
        }

        [HttpGet("task/{taskId}")]
        public async Task<ActionResult<IEnumerable<IssueDto>>> GetIssuesByTask(Guid taskId)
        {
            var issues = await _issueRepository.GetByTaskIdAsync(taskId);
            var issueDtos = issues.Select(MapToDto);
            return Ok(issueDtos);
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<IssueDto>>> GetIssuesByStatus(string status)
        {
            var issues = await _issueRepository.GetByStatusAsync(status);
            var issueDtos = issues.Select(MapToDto);
            return Ok(issueDtos);
        }

        [HttpGet("priority/{priority}")]
        public async Task<ActionResult<IEnumerable<IssueDto>>> GetIssuesByPriority(string priority)
        {
            var issues = await _issueRepository.GetByPriorityAsync(priority);
            var issueDtos = issues.Select(MapToDto);
            return Ok(issueDtos);
        }

        [HttpGet("severity/{severity}")]
        public async Task<ActionResult<IEnumerable<IssueDto>>> GetIssuesBySeverity(string severity)
        {
            var issues = await _issueRepository.GetBySeverityAsync(severity);
            var issueDtos = issues.Select(MapToDto);
            return Ok(issueDtos);
        }

        [HttpGet("assigned/{userId}")]
        public async Task<ActionResult<IEnumerable<IssueDto>>> GetIssuesByAssignedUser(Guid userId)
        {
            var issues = await _issueRepository.GetByAssignedUserIdAsync(userId);
            var issueDtos = issues.Select(MapToDto);
            return Ok(issueDtos);
        }

        [HttpGet("type/{issueType}")]
        public async Task<ActionResult<IEnumerable<IssueDto>>> GetIssuesByType(string issueType)
        {
            var issues = await _issueRepository.GetByIssueTypeAsync(issueType);
            var issueDtos = issues.Select(MapToDto);
            return Ok(issueDtos);
        }

        [HttpGet("open")]
        public async Task<ActionResult<IEnumerable<IssueDto>>> GetOpenIssues()
        {
            var issues = await _issueRepository.GetOpenIssuesAsync();
            var issueDtos = issues.Select(MapToDto);
            return Ok(issueDtos);
        }

        [HttpGet("critical")]
        public async Task<ActionResult<IEnumerable<IssueDto>>> GetCriticalIssues()
        {
            var issues = await _issueRepository.GetCriticalIssuesAsync();
            var issueDtos = issues.Select(MapToDto);
            return Ok(issueDtos);
        }

        [HttpGet("overdue")]
        public async Task<ActionResult<IEnumerable<IssueDto>>> GetOverdueIssues()
        {
            var issues = await _issueRepository.GetOverdueIssuesAsync();
            var issueDtos = issues.Select(MapToDto);
            return Ok(issueDtos);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<IssueDto>>> SearchIssues([FromQuery] string searchTerm)
        {
            var issues = await _issueRepository.SearchByDescriptionAsync(searchTerm);
            var issueDtos = issues.Select(MapToDto);
            return Ok(issueDtos);
        }

        [HttpGet("tenant/{tenantId}")]
        public async Task<ActionResult<IEnumerable<IssueDto>>> GetIssuesByTenant(Guid tenantId)
        {
            var issues = await _issueRepository.GetByTenantIdAsync(tenantId);
            var issueDtos = issues.Select(MapToDto);
            return Ok(issueDtos);
        }

        [HttpGet("milestone/{milestoneId}")]
        public async Task<ActionResult<IEnumerable<IssueDto>>> GetIssuesByMilestone(Guid milestoneId)
        {
            var issues = await _issueRepository.GetByMilestoneIdAsync(milestoneId);
            var issueDtos = issues.Select(MapToDto);
            return Ok(issueDtos);
        }

        [HttpGet("tenant/{tenantId}/status/{status}")]
        public async Task<ActionResult<IEnumerable<IssueDto>>> GetIssuesByTenantAndStatus(Guid tenantId, string status)
        {
            var issues = await _issueRepository.GetByTenantAndStatusAsync(tenantId, status);
            var issueDtos = issues.Select(MapToDto);
            return Ok(issueDtos);
        }

        [HttpGet("project/{projectId}/status/{status}")]
        public async Task<ActionResult<IEnumerable<IssueDto>>> GetIssuesByProjectAndStatus(Guid projectId, string status)
        {
            var issues = await _issueRepository.GetByProjectAndStatusAsync(projectId, status);
            var issueDtos = issues.Select(MapToDto);
            return Ok(issueDtos);
        }

        [HttpGet("tenant/{tenantId}/priority/{priority}")]
        public async Task<ActionResult<IEnumerable<IssueDto>>> GetIssuesByTenantAndPriority(Guid tenantId, string priority)
        {
            var issues = await _issueRepository.GetByTenantAndPriorityAsync(tenantId, priority);
            var issueDtos = issues.Select(MapToDto);
            return Ok(issueDtos);
        }

        [HttpGet("tenant/{tenantId}/severity/{severity}")]
        public async Task<ActionResult<IEnumerable<IssueDto>>> GetIssuesByTenantAndSeverity(Guid tenantId, string severity)
        {
            var issues = await _issueRepository.GetByTenantAndSeverityAsync(tenantId, severity);
            var issueDtos = issues.Select(MapToDto);
            return Ok(issueDtos);
        }

        [HttpPost]
        public async Task<ActionResult<IssueDto>> CreateIssue(CreateIssueDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var issue = MapToEntity(createDto);
            var createdIssue = await _issueRepository.AddAsync(issue);
            return CreatedAtAction(nameof(GetIssue), new { id = createdIssue.IssueId }, MapToDto(createdIssue));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIssue(Guid id, UpdateIssueDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingIssue = await _issueRepository.GetByIdAsync(id);
            if (existingIssue == null)
            {
                return NotFound();
            }

            UpdateEntityFromDto(existingIssue, updateDto);
            var updatedIssue = await _issueRepository.UpdateAsync(existingIssue);
            return Ok(MapToDto(updatedIssue));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIssue(Guid id)
        {
            var deleted = await _issueRepository.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        private static IssueDto MapToDto(Issue issue)
        {
            return new IssueDto
            {
                IssueId = issue.IssueId,
                ProjectId = issue.ProjectId,
                TaskId = issue.TaskId,
                IssueName = issue.IssueName,
                IssueDescription = issue.IssueDescription,
                IssueType = issue.IssueType,
                RootCause = issue.RootCause,
                ImpactOnScheduleDays = issue.ImpactOnScheduleDays,
                ImpactOnBudgetUsd = issue.ImpactOnBudgetUsd,
                ImpactOnScope = issue.ImpactOnScope,
                ImpactOnQuality = issue.ImpactOnQuality,
                ResolutionSteps = issue.ResolutionSteps,
                Severity = issue.Severity,
                Priority = issue.Priority,
                AssignedToUserId = issue.AssignedToUserId,
                Status = issue.Status,
                OpenedDate = issue.OpenedDate,
                ClosedDate = issue.ClosedDate,
                CreatedAt = issue.CreatedAt,
                UpdatedAt = issue.UpdatedAt,
                TenantId = issue.TenantId,
                MilestoneId = issue.MilestoneId,
                Project = issue.Project != null ? MapProjectToDto(issue.Project) : null,
                Task = issue.Task != null ? MapTaskToDto(issue.Task) : null,
                AssignedToUser = issue.AssignedToUser != null ? MapUserToDto(issue.AssignedToUser) : null,
                Tenant = issue.Tenant != null ? MapOrganizationToDto(issue.Tenant) : null,
                Milestone = issue.Milestone != null ? MapMilestoneToDto(issue.Milestone) : null
            };
        }

        private static ProjectDto MapProjectToDto(Project project)
        {
            return new ProjectDto
            {
                ProjectId = project.ProjectId,
                ProjectName = project.ProjectName,
                ProjectType = project.ProjectType,
                IndustryDomain = project.IndustryDomain,
                PlannedStartDate = project.PlannedStartDate,
                PlannedEndDate = project.PlannedEndDate,
                ActualStartDate = project.ActualStartDate,
                ActualEndDate = project.ActualEndDate,
                PlannedBudget = project.PlannedBudget,
                ActualBudget = project.ActualBudget,
                ProjectManagerId = project.ProjectManagerId,
                TeamSize = project.TeamSize,
                ContractType = project.ContractType,
                ProjectStatus = project.ProjectStatus,
                ProjectComplexity = project.ProjectComplexity,
                CreatedAt = project.CreatedAt,
                UpdatedAt = project.UpdatedAt,
                Description = project.Description,
                Notes = project.Notes,
                CurrentPhase = project.CurrentPhase,
                OrganizationId = project.OrganizationId
            };
        }

        private static TaskDto MapTaskToDto(Task task)
        {
            return new TaskDto
            {
                TaskId = task.TaskId,
                ProjectId = task.ProjectId,
                TaskName = task.TaskName,
                TaskType = task.TaskType,
                AssignedToUserId = task.AssignedToUserId,
                ParentTaskId = task.ParentTaskId,
                PlannedEffortHours = task.PlannedEffortHours,
                ActualEffortHours = task.ActualEffortHours,
                PlannedStartDate = task.PlannedStartDate,
                PlannedEndDate = task.PlannedEndDate,
                ActualStartDate = task.ActualStartDate,
                ActualEndDate = task.ActualEndDate,
                TaskStatus = task.TaskStatus,
                Priority = task.Priority,
                IsMilestone = task.IsMilestone,
                Description = task.Description,
                Comments = task.Comments,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt
            };
        }

        private static UserDto MapUserToDto(User user)
        {
            return new UserDto
            {
                UserId = user.UserId,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }

        private static OrganizationDto MapOrganizationToDto(Organization organization)
        {
            return new OrganizationDto
            {
                OrganizationId = organization.OrganizationId,
                OrganizationName = organization.OrganizationName,
                Status = organization.Status,
                BillingPlan = organization.BillingPlan,
                ContactEmail = organization.ContactEmail,
                CreatedAt = organization.CreatedAt,
                UpdatedAt = organization.UpdatedAt
            };
        }

        private static MilestoneDto MapMilestoneToDto(Milestone milestone)
        {
            return new MilestoneDto
            {
                MilestoneId = milestone.MilestoneId,
                ProjectId = milestone.ProjectId,
                MilestoneName = milestone.MilestoneName,
                Description = milestone.Description,
                DueDate = milestone.DueDate,
                IsAchieved = milestone.IsAchieved,
                CompletionDate = milestone.CompletionDate,
                TenantId = milestone.TenantId,
                CreatedAt = milestone.CreatedAt,
                UpdatedAt = milestone.UpdatedAt
            };
        }

        private static Issue MapToEntity(CreateIssueDto dto)
        {
            return new Issue
            {
                ProjectId = dto.ProjectId,
                TaskId = dto.TaskId,
                IssueName = dto.IssueName,
                IssueDescription = dto.IssueDescription,
                IssueType = dto.IssueType,
                RootCause = dto.RootCause,
                ImpactOnScheduleDays = dto.ImpactOnScheduleDays,
                ImpactOnBudgetUsd = dto.ImpactOnBudgetUsd,
                ImpactOnScope = dto.ImpactOnScope,
                ImpactOnQuality = dto.ImpactOnQuality,
                ResolutionSteps = dto.ResolutionSteps,
                Severity = dto.Severity,
                Priority = dto.Priority,
                AssignedToUserId = dto.AssignedToUserId,
                Status = dto.Status,
                OpenedDate = dto.OpenedDate,
                ClosedDate = dto.ClosedDate,
                TenantId = dto.TenantId,
                MilestoneId = dto.MilestoneId
            };
        }

        private static void UpdateEntityFromDto(Issue issue, UpdateIssueDto dto)
        {
            if (dto.TaskId.HasValue)
                issue.TaskId = dto.TaskId;
            if (dto.IssueName != null)
                issue.IssueName = dto.IssueName;
            if (dto.IssueDescription != null)
                issue.IssueDescription = dto.IssueDescription;
            if (dto.IssueType != null)
                issue.IssueType = dto.IssueType;
            if (dto.RootCause != null)
                issue.RootCause = dto.RootCause;
            if (dto.ImpactOnScheduleDays.HasValue)
                issue.ImpactOnScheduleDays = dto.ImpactOnScheduleDays;
            if (dto.ImpactOnBudgetUsd.HasValue)
                issue.ImpactOnBudgetUsd = dto.ImpactOnBudgetUsd;
            if (dto.ImpactOnScope != null)
                issue.ImpactOnScope = dto.ImpactOnScope;
            if (dto.ImpactOnQuality != null)
                issue.ImpactOnQuality = dto.ImpactOnQuality;
            if (dto.ResolutionSteps != null)
                issue.ResolutionSteps = dto.ResolutionSteps;
            if (dto.Severity != null)
                issue.Severity = dto.Severity;
            if (dto.Priority != null)
                issue.Priority = dto.Priority;
            if (dto.AssignedToUserId.HasValue)
                issue.AssignedToUserId = dto.AssignedToUserId;
            if (dto.Status != null)
                issue.Status = dto.Status;
            if (dto.OpenedDate.HasValue)
                issue.OpenedDate = dto.OpenedDate.Value;
            if (dto.ClosedDate.HasValue)
                issue.ClosedDate = dto.ClosedDate;
            if (dto.MilestoneId.HasValue)
                issue.MilestoneId = dto.MilestoneId;
        }
    }
} 