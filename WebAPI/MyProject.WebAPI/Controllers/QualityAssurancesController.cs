using Microsoft.AspNetCore.Mvc;
using MyProject.Persistence.Entities;
using MyProject.Persistence.Repositories;
using MyProject.WebAPI.DTOs;
using Task = MyProject.Persistence.Entities.Task;
namespace MyProject.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QualityAssurancesController : ControllerBase
    {
        private readonly IQualityAssuranceRepository _qualityAssuranceRepository;

        public QualityAssurancesController(IQualityAssuranceRepository qualityAssuranceRepository)
        {
            _qualityAssuranceRepository = qualityAssuranceRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QualityAssuranceDto>>> GetQualityAssurances()
        {
            var qualityAssurances = await _qualityAssuranceRepository.GetAllAsync();
            var qualityAssuranceDtos = qualityAssurances.Select(MapToDto);
            return Ok(qualityAssuranceDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QualityAssuranceDto>> GetQualityAssurance(Guid id)
        {
            var qualityAssurance = await _qualityAssuranceRepository.GetByIdAsync(id);
            if (qualityAssurance == null)
            {
                return NotFound();
            }
            return Ok(MapToDto(qualityAssurance));
        }

        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<IEnumerable<QualityAssuranceDto>>> GetQualityAssurancesByProject(Guid projectId)
        {
            var qualityAssurances = await _qualityAssuranceRepository.GetByProjectIdAsync(projectId);
            var qualityAssuranceDtos = qualityAssurances.Select(MapToDto);
            return Ok(qualityAssuranceDtos);
        }

        [HttpGet("task/{taskId}")]
        public async Task<ActionResult<IEnumerable<QualityAssuranceDto>>> GetQualityAssurancesByTask(Guid taskId)
        {
            var qualityAssurances = await _qualityAssuranceRepository.GetByTaskIdAsync(taskId);
            var qualityAssuranceDtos = qualityAssurances.Select(MapToDto);
            return Ok(qualityAssuranceDtos);
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<QualityAssuranceDto>>> GetQualityAssurancesByStatus(string status)
        {
            var qualityAssurances = await _qualityAssuranceRepository.GetByStatusAsync(status);
            var qualityAssuranceDtos = qualityAssurances.Select(MapToDto);
            return Ok(qualityAssuranceDtos);
        }

        [HttpGet("type/{itemType}")]
        public async Task<ActionResult<IEnumerable<QualityAssuranceDto>>> GetQualityAssurancesByType(string itemType)
        {
            var qualityAssurances = await _qualityAssuranceRepository.GetByItemTypeAsync(itemType);
            var qualityAssuranceDtos = qualityAssurances.Select(MapToDto);
            return Ok(qualityAssuranceDtos);
        }

        [HttpGet("severity/{severity}")]
        public async Task<ActionResult<IEnumerable<QualityAssuranceDto>>> GetQualityAssurancesBySeverity(string severity)
        {
            var qualityAssurances = await _qualityAssuranceRepository.GetBySeverityAsync(severity);
            var qualityAssuranceDtos = qualityAssurances.Select(MapToDto);
            return Ok(qualityAssuranceDtos);
        }

        [HttpGet("priority/{priority}")]
        public async Task<ActionResult<IEnumerable<QualityAssuranceDto>>> GetQualityAssurancesByPriority(string priority)
        {
            var qualityAssurances = await _qualityAssuranceRepository.GetByPriorityAsync(priority);
            var qualityAssuranceDtos = qualityAssurances.Select(MapToDto);
            return Ok(qualityAssuranceDtos);
        }

        [HttpGet("reported-by/{userId}")]
        public async Task<ActionResult<IEnumerable<QualityAssuranceDto>>> GetQualityAssurancesByReporter(Guid userId)
        {
            var qualityAssurances = await _qualityAssuranceRepository.GetByReportedByUserIdAsync(userId);
            var qualityAssuranceDtos = qualityAssurances.Select(MapToDto);
            return Ok(qualityAssuranceDtos);
        }

        [HttpGet("assigned-to/{userId}")]
        public async Task<ActionResult<IEnumerable<QualityAssuranceDto>>> GetQualityAssurancesByAssignee(Guid userId)
        {
            var qualityAssurances = await _qualityAssuranceRepository.GetByAssignedToUserIdAsync(userId);
            var qualityAssuranceDtos = qualityAssurances.Select(MapToDto);
            return Ok(qualityAssuranceDtos);
        }

        [HttpGet("open")]
        public async Task<ActionResult<IEnumerable<QualityAssuranceDto>>> GetOpenQualityAssurances()
        {
            var qualityAssurances = await _qualityAssuranceRepository.GetOpenItemsAsync();
            var qualityAssuranceDtos = qualityAssurances.Select(MapToDto);
            return Ok(qualityAssuranceDtos);
        }

        [HttpGet("closed")]
        public async Task<ActionResult<IEnumerable<QualityAssuranceDto>>> GetClosedQualityAssurances()
        {
            var qualityAssurances = await _qualityAssuranceRepository.GetClosedItemsAsync();
            var qualityAssuranceDtos = qualityAssurances.Select(MapToDto);
            return Ok(qualityAssuranceDtos);
        }

        [HttpGet("date-range")]
        public async Task<ActionResult<IEnumerable<QualityAssuranceDto>>> GetQualityAssurancesByDateRange(
            [FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate)
        {
            var qualityAssurances = await _qualityAssuranceRepository.GetByReportedDateRangeAsync(startDate, endDate);
            var qualityAssuranceDtos = qualityAssurances.Select(MapToDto);
            return Ok(qualityAssuranceDtos);
        }

        [HttpGet("environment/{testEnvironment}")]
        public async Task<ActionResult<IEnumerable<QualityAssuranceDto>>> GetQualityAssurancesByEnvironment(string testEnvironment)
        {
            var qualityAssurances = await _qualityAssuranceRepository.GetByTestEnvironmentAsync(testEnvironment);
            var qualityAssuranceDtos = qualityAssurances.Select(MapToDto);
            return Ok(qualityAssuranceDtos);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<QualityAssuranceDto>>> SearchQualityAssurances([FromQuery] string searchTerm)
        {
            var qualityAssurances = await _qualityAssuranceRepository.SearchBySummaryAsync(searchTerm);
            var qualityAssuranceDtos = qualityAssurances.Select(MapToDto);
            return Ok(qualityAssuranceDtos);
        }

        [HttpPost]
        public async Task<ActionResult<QualityAssuranceDto>> CreateQualityAssurance(CreateQualityAssuranceDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var qualityAssurance = MapToEntity(createDto);
            var createdQualityAssurance = await _qualityAssuranceRepository.AddAsync(qualityAssurance);
            return CreatedAtAction(nameof(GetQualityAssurance), new { id = createdQualityAssurance.QaItemId }, MapToDto(createdQualityAssurance));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQualityAssurance(Guid id, UpdateQualityAssuranceDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingQualityAssurance = await _qualityAssuranceRepository.GetByIdAsync(id);
            if (existingQualityAssurance == null)
            {
                return NotFound();
            }

            UpdateEntityFromDto(existingQualityAssurance, updateDto);
            var updatedQualityAssurance = await _qualityAssuranceRepository.UpdateAsync(existingQualityAssurance);
            return Ok(MapToDto(updatedQualityAssurance));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQualityAssurance(Guid id)
        {
            var deleted = await _qualityAssuranceRepository.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        private static QualityAssuranceDto MapToDto(QualityAssurance qualityAssurance)
        {
            return new QualityAssuranceDto
            {
                QaItemId = qualityAssurance.QaItemId,
                ProjectId = qualityAssurance.ProjectId,
                TaskId = qualityAssurance.TaskId,
                ItemType = qualityAssurance.ItemType,
                Summary = qualityAssurance.Summary,
                Description = qualityAssurance.Description,
                Severity = qualityAssurance.Severity,
                Priority = qualityAssurance.Priority,
                Status = qualityAssurance.Status,
                ReportedByUserId = qualityAssurance.ReportedByUserId,
                AssignedToUserId = qualityAssurance.AssignedToUserId,
                ReportedDate = qualityAssurance.ReportedDate,
                ResolutionDate = qualityAssurance.ResolutionDate,
                ClosedDate = qualityAssurance.ClosedDate,
                ResolutionNotes = qualityAssurance.ResolutionNotes,
                TestEnvironment = qualityAssurance.TestEnvironment,
                CreatedAt = qualityAssurance.CreatedAt,
                UpdatedAt = qualityAssurance.UpdatedAt,
                Project = qualityAssurance.Project != null ? MapProjectToDto(qualityAssurance.Project) : null,
                Task = qualityAssurance.Task != null ? MapTaskToDto(qualityAssurance.Task) : null
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

        private static QualityAssurance MapToEntity(CreateQualityAssuranceDto dto)
        {
            return new QualityAssurance
            {
                ProjectId = dto.ProjectId,
                TaskId = dto.TaskId,
                ItemType = dto.ItemType,
                Summary = dto.Summary,
                Description = dto.Description,
                Severity = dto.Severity,
                Priority = dto.Priority,
                Status = dto.Status,
                ReportedByUserId = dto.ReportedByUserId,
                AssignedToUserId = dto.AssignedToUserId,
                ReportedDate = dto.ReportedDate,
                ResolutionDate = dto.ResolutionDate,
                ClosedDate = dto.ClosedDate,
                ResolutionNotes = dto.ResolutionNotes,
                TestEnvironment = dto.TestEnvironment
            };
        }

        private static void UpdateEntityFromDto(QualityAssurance qualityAssurance, UpdateQualityAssuranceDto dto)
        {
            if (dto.TaskId.HasValue)
                qualityAssurance.TaskId = dto.TaskId;
            if (dto.ItemType != null)
                qualityAssurance.ItemType = dto.ItemType;
            if (dto.Summary != null)
                qualityAssurance.Summary = dto.Summary;
            if (dto.Description != null)
                qualityAssurance.Description = dto.Description;
            if (dto.Severity != null)
                qualityAssurance.Severity = dto.Severity;
            if (dto.Priority != null)
                qualityAssurance.Priority = dto.Priority;
            if (dto.Status != null)
                qualityAssurance.Status = dto.Status;
            if (dto.ReportedByUserId.HasValue)
                qualityAssurance.ReportedByUserId = dto.ReportedByUserId;
            if (dto.AssignedToUserId.HasValue)
                qualityAssurance.AssignedToUserId = dto.AssignedToUserId;
            if (dto.ResolutionDate.HasValue)
                qualityAssurance.ResolutionDate = dto.ResolutionDate;
            if (dto.ClosedDate.HasValue)
                qualityAssurance.ClosedDate = dto.ClosedDate;
            if (dto.ResolutionNotes != null)
                qualityAssurance.ResolutionNotes = dto.ResolutionNotes;
            if (dto.TestEnvironment != null)
                qualityAssurance.TestEnvironment = dto.TestEnvironment;
        }
    }
} 