using Microsoft.AspNetCore.Mvc;
using MyProject.Persistence.Entities;
using MyProject.Persistence.Repositories;
using MyProject.WebAPI.DTOs;

namespace MyProject.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskHistoriesController : ControllerBase
{
    private readonly ITaskHistoryRepository _taskHistoryRepository;

    public TaskHistoriesController(ITaskHistoryRepository taskHistoryRepository)
    {
        _taskHistoryRepository = taskHistoryRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskHistoryDto>>> GetAll()
    {
        try
        {
            var taskHistories = await _taskHistoryRepository.GetAllAsync();
            var dtos = taskHistories.Select(MapToDto);
            return Ok(dtos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TaskHistoryDto>> GetById(Guid id)
    {
        try
        {
            var taskHistory = await _taskHistoryRepository.GetByIdAsync(id);
            if (taskHistory == null)
                return NotFound();

            return Ok(MapToDto(taskHistory));
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("task/{taskId:guid}")]
    public async Task<ActionResult<IEnumerable<TaskHistoryDto>>> GetByTaskId(Guid taskId)
    {
        try
        {
            var taskHistories = await _taskHistoryRepository.GetByTaskIdAsync(taskId);
            var dtos = taskHistories.Select(MapToDto);
            return Ok(dtos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("user/{userId:guid}")]
    public async Task<ActionResult<IEnumerable<TaskHistoryDto>>> GetByUserId(Guid userId)
    {
        try
        {
            var taskHistories = await _taskHistoryRepository.GetByUserIdAsync(userId);
            var dtos = taskHistories.Select(MapToDto);
            return Ok(dtos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("tenant/{tenantId:guid}")]
    public async Task<ActionResult<IEnumerable<TaskHistoryDto>>> GetByTenantId(Guid tenantId)
    {
        try
        {
            var taskHistories = await _taskHistoryRepository.GetByTenantIdAsync(tenantId);
            var dtos = taskHistories.Select(MapToDto);
            return Ok(dtos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult<TaskHistoryDto>> Create(CreateTaskHistoryDto createDto)
    {
        try
        {
            var taskHistory = new TaskHistory
            {
                TaskId = createDto.TaskId,
                UserId = createDto.UserId,
                Comment = createDto.Comment,
                OrganizationId = createDto.OrganizationId
            };

            var created = await _taskHistoryRepository.CreateAsync(taskHistory);
            return CreatedAtAction(nameof(GetById), new { id = created.TaskHistoryId }, MapToDto(created));
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<TaskHistoryDto>> Update(Guid id, UpdateTaskHistoryDto updateDto)
    {
        try
        {
            var taskHistory = await _taskHistoryRepository.GetByIdAsync(id);
            if (taskHistory == null)
                return NotFound();

            taskHistory.Comment = updateDto.Comment;
            var updated = await _taskHistoryRepository.UpdateAsync(taskHistory);
            return Ok(MapToDto(updated));
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            var success = await _taskHistoryRepository.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("task/{taskId:guid}")]
    public async Task<ActionResult> DeleteByTaskId(Guid taskId)
    {
        try
        {
            var success = await _taskHistoryRepository.DeleteByTaskIdAsync(taskId);
            if (!success)
                return NotFound();

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    private static TaskHistoryDto MapToDto(TaskHistory taskHistory)
    {
        return new TaskHistoryDto
        {
            TaskHistoryId = taskHistory.TaskHistoryId,
            TaskId = taskHistory.TaskId,
            UserId = taskHistory.UserId,
            Comment = taskHistory.Comment,
            CreatedAt = taskHistory.CreatedAt,
            //UpdatedAt = taskHistory.UpdatedAt,
            TenantId = taskHistory.OrganizationId,
            Task = taskHistory.Task != null ? new TaskDto
            {
                TaskId = taskHistory.Task.TaskId,
                TaskName = taskHistory.Task.TaskName,
                Description = taskHistory.Task.Description,
                TaskStatus = taskHistory.Task.TaskStatus,
                Priority = taskHistory.Task.Priority,
                PlannedStartDate = taskHistory.Task.PlannedStartDate,
                PlannedEndDate = taskHistory.Task.PlannedEndDate,
                ActualStartDate = taskHistory.Task.ActualStartDate,
                ActualEndDate = taskHistory.Task.ActualEndDate,
               // ProgressPercentage = taskHistory.Task.ProgressPercentage,
               // PlannedEffort = taskHistory.Task.PlannedEffort,
               // ActualEffort = taskHistory.Task.ActualEffort,
               // Notes = taskHistory.Task.Notes,
                CreatedAt = taskHistory.Task.CreatedAt,
                UpdatedAt = taskHistory.Task.UpdatedAt,
                ProjectId = taskHistory.Task.ProjectId,
                MilestoneId = taskHistory.Task.MilestoneId,
                AssignedToUserId = taskHistory.Task.AssignedToUserId,
                ParentTaskId = taskHistory.Task.ParentTaskId,
                IssueId = taskHistory.Task.IssueId,
                OrganizationId = taskHistory.Task.OrganizationId
            } : null,
            User = taskHistory.User != null ? new UserDto
            {
                UserId = taskHistory.User.UserId,
                FirstName = taskHistory.User.FirstName,
                LastName = taskHistory.User.LastName,
                Email = taskHistory.User.Email,
                //Phone = taskHistory.User.Phone,
                CreatedAt = taskHistory.User.CreatedAt,
                UpdatedAt = taskHistory.User.UpdatedAt,
                //TenantId = taskHistory.User.TenantId
            } : null,
            Organization = taskHistory.Organization != null ? new OrganizationDto
            {
                OrganizationId = taskHistory.Organization.OrganizationId,
                OrganizationName = taskHistory.Organization.OrganizationName,
                Status = taskHistory.Organization.Status,
                BillingPlan = taskHistory.Organization.BillingPlan,
                ContactEmail = taskHistory.Organization.ContactEmail,
                CreatedAt = taskHistory.Organization.CreatedAt,
                UpdatedAt = taskHistory.Organization.UpdatedAt
            } : null
        };
    }
} 