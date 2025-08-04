using Microsoft.AspNetCore.Mvc;
using MyProject.Persistence.Entities;
using MyProject.Persistence.Repositories;
using MyProject.WebAPI.DTOs;
using Task = MyProject.Persistence.Entities.Task;
namespace MyProject.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TasksController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasks()
        {
            var tasks = await _taskRepository.GetAllAsync();
            var taskDtos = tasks.Select(MapToDto);
            return Ok(taskDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetTask(Guid id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(MapToDto(task));
        }

        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasksByProject(Guid projectId)
        {
            var tasks = await _taskRepository.GetByProjectIdAsync(projectId);
            var taskDtos = tasks.Select(MapToDto);
            return Ok(taskDtos);
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasksByStatus(string status)
        {
            var tasks = await _taskRepository.GetByStatusAsync(status);
            var taskDtos = tasks.Select(MapToDto);
            return Ok(taskDtos);
        }

        [HttpGet("priority/{priority}")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasksByPriority(string priority)
        {
            var tasks = await _taskRepository.GetByPriorityAsync(priority);
            var taskDtos = tasks.Select(MapToDto);
            return Ok(taskDtos);
        }

        [HttpGet("assigned/{userId}")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasksByAssignedUser(Guid userId)
        {
            var tasks = await _taskRepository.GetByAssignedUserIdAsync(userId);
            var taskDtos = tasks.Select(MapToDto);
            return Ok(taskDtos);
        }

        [HttpGet("parent/{parentTaskId}")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetSubtasks(Guid parentTaskId)
        {
            var tasks = await _taskRepository.GetByParentTaskIdAsync(parentTaskId);
            var taskDtos = tasks.Select(MapToDto);
            return Ok(taskDtos);
        }

        [HttpGet("milestones")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetMilestones()
        {
            var tasks = await _taskRepository.GetMilestonesAsync();
            var taskDtos = tasks.Select(MapToDto);
            return Ok(taskDtos);
        }

        [HttpGet("overdue")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetOverdueTasks()
        {
            var tasks = await _taskRepository.GetOverdueTasksAsync();
            var taskDtos = tasks.Select(MapToDto);
            return Ok(taskDtos);
        }

        [HttpGet("with-subtasks")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasksWithSubtasks()
        {
            var tasks = await _taskRepository.GetTasksWithSubtasksAsync();
            var taskDtos = tasks.Select(MapToDto);
            return Ok(taskDtos);
        }

        [HttpGet("tenant/{tenantId}")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasksByTenant(Guid tenantId)
        {
            var tasks = await _taskRepository.GetByTenantIdAsync(tenantId);
            var taskDtos = tasks.Select(MapToDto);
            return Ok(taskDtos);
        }

        [HttpGet("tenant/{tenantId}/status/{status}")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasksByTenantAndStatus(Guid tenantId, string status)
        {
            var tasks = await _taskRepository.GetByTenantAndStatusAsync(tenantId, status);
            var taskDtos = tasks.Select(MapToDto);
            return Ok(taskDtos);
        }

        [HttpGet("tenant/{tenantId}/priority/{priority}")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasksByTenantAndPriority(Guid tenantId, string priority)
        {
            var tasks = await _taskRepository.GetByTenantAndPriorityAsync(tenantId, priority);
            var taskDtos = tasks.Select(MapToDto);
            return Ok(taskDtos);
        }

        [HttpGet("project/{projectId}/status/{status}")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasksByProjectAndStatus(Guid projectId, string status)
        {
            var tasks = await _taskRepository.GetByProjectAndStatusAsync(projectId, status);
            var taskDtos = tasks.Select(MapToDto);
            return Ok(taskDtos);
        }

        [HttpGet("milestone/{milestoneId}")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasksByMilestone(Guid milestoneId)
        {
            var tasks = await _taskRepository.GetByMilestoneIdAsync(milestoneId);
            var taskDtos = tasks.Select(MapToDto);
            return Ok(taskDtos);
        }

        [HttpGet("tenant/{tenantId}/user/{userId}")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasksByTenantAndUser(Guid tenantId, Guid userId)
        {
            var tasks = await _taskRepository.GetByTenantAndUserAsync(tenantId, userId);
            var taskDtos = tasks.Select(MapToDto);
            return Ok(taskDtos);
        }

        [HttpGet("tenant/{tenantId}/overdue")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetOverdueTasksByTenant(Guid tenantId)
        {
            var tasks = await _taskRepository.GetOverdueTasksByTenantAsync(tenantId);
            var taskDtos = tasks.Select(MapToDto);
            return Ok(taskDtos);
        }

        [HttpGet("tenant/{tenantId}/milestones")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetMilestonesByTenant(Guid tenantId)
        {
            var tasks = await _taskRepository.GetMilestonesByTenantAsync(tenantId);
            var taskDtos = tasks.Select(MapToDto);
            return Ok(taskDtos);
        }

        [HttpPost]
        public async Task<ActionResult<TaskDto>> CreateTask(CreateTaskDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var task = MapToEntity(createDto);
            var createdTask = await _taskRepository.AddAsync(task);
            return CreatedAtAction(nameof(GetTask), new { id = createdTask.TaskId }, MapToDto(createdTask));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(Guid id, UpdateTaskDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingTask = await _taskRepository.GetByIdAsync(id);
            if (existingTask == null)
            {
                return NotFound();
            }

            UpdateEntityFromDto(existingTask, updateDto);
            var updatedTask = await _taskRepository.UpdateAsync(existingTask);
            return Ok(MapToDto(updatedTask));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var deleted = await _taskRepository.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        private static TaskDto MapToDto(Task task)
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
                UpdatedAt = task.UpdatedAt,
                TenantId = task.TenantId,
                MilestoneId = task.MilestoneId,
                Project = task.Project != null ? MapProjectToDto(task.Project) : null,
                ParentTask = task.ParentTask != null ? MapToDto(task.ParentTask) : null,
                Subtasks = task.Subtasks?.Select(MapToDto).ToList(),
                AssignedToUser = task.AssignedToUser != null ? MapUserToDto(task.AssignedToUser) : null,
                Tenant = task.Tenant != null ? MapOrganizationToDto(task.Tenant) : null,
                Milestone = task.Milestone != null ? MapMilestoneToDto(task.Milestone) : null
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
                IsAchieved = milestone.IsAchieved,
                CreatedAt = milestone.CreatedAt,
                UpdatedAt = milestone.UpdatedAt,
                TenantId = milestone.TenantId
            };
        }

        private static Task MapToEntity(CreateTaskDto dto)
        {
            return new Task
            {
                ProjectId = dto.ProjectId,
                TaskName = dto.TaskName,
                TaskType = dto.TaskType,
                AssignedToUserId = dto.AssignedToUserId,
                ParentTaskId = dto.ParentTaskId,
                PlannedEffortHours = dto.PlannedEffortHours,
                ActualEffortHours = dto.ActualEffortHours,
                PlannedStartDate = dto.PlannedStartDate,
                PlannedEndDate = dto.PlannedEndDate,
                ActualStartDate = dto.ActualStartDate,
                ActualEndDate = dto.ActualEndDate,
                TaskStatus = dto.TaskStatus,
                Priority = dto.Priority,
                IsMilestone = dto.IsMilestone,
                Description = dto.Description,
                Comments = dto.Comments,
                TenantId = dto.TenantId,
                MilestoneId = dto.MilestoneId
            };
        }

        private static void UpdateEntityFromDto(Task task, UpdateTaskDto dto)
        {
            if (dto.TaskName != null)
                task.TaskName = dto.TaskName;
            if (dto.TaskType != null)
                task.TaskType = dto.TaskType;
            if (dto.AssignedToUserId.HasValue)
                task.AssignedToUserId = dto.AssignedToUserId;
            if (dto.ParentTaskId.HasValue)
                task.ParentTaskId = dto.ParentTaskId;
            if (dto.PlannedEffortHours.HasValue)
                task.PlannedEffortHours = dto.PlannedEffortHours;
            if (dto.ActualEffortHours.HasValue)
                task.ActualEffortHours = dto.ActualEffortHours;
            if (dto.PlannedStartDate.HasValue)
                task.PlannedStartDate = dto.PlannedStartDate;
            if (dto.PlannedEndDate.HasValue)
                task.PlannedEndDate = dto.PlannedEndDate;
            if (dto.ActualStartDate.HasValue)
                task.ActualStartDate = dto.ActualStartDate;
            if (dto.ActualEndDate.HasValue)
                task.ActualEndDate = dto.ActualEndDate;
            if (dto.TaskStatus != null)
                task.TaskStatus = dto.TaskStatus;
            if (dto.Priority != null)
                task.Priority = dto.Priority;
            if (dto.IsMilestone.HasValue)
                task.IsMilestone = dto.IsMilestone.Value;
            if (dto.Description != null)
                task.Description = dto.Description;
            if (dto.Comments != null)
                task.Comments = dto.Comments;
        }
    }
} 