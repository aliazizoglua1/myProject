using Microsoft.AspNetCore.Mvc;
using MyProject.Persistence.Entities;
using MyProject.Persistence.Repositories;
using MyProject.WebAPI.DTOs;

namespace MyProject.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskTimeLogsController : ControllerBase
    {
        private readonly ITaskTimeLogRepository _taskTimeLogRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IResourceRepository _resourceRepository;

        public TaskTimeLogsController(
            ITaskTimeLogRepository taskTimeLogRepository,
            ITaskRepository taskRepository,
            IResourceRepository resourceRepository)
        {
            _taskTimeLogRepository = taskTimeLogRepository;
            _taskRepository = taskRepository;
            _resourceRepository = resourceRepository;
        }

        // GET: api/tasktimelogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskTimeLogDto>>> GetTaskTimeLogs()
        {
            var taskTimeLogs = await _taskTimeLogRepository.GetAllAsync();
            var dtos = taskTimeLogs.Select(MapToDto);
            return Ok(dtos);
        }

        // GET: api/tasktimelogs/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskTimeLogDto>> GetTaskTimeLog(Guid id)
        {
            var taskTimeLog = await _taskTimeLogRepository.GetByIdAsync(id);
            if (taskTimeLog == null)
            {
                return NotFound();
            }

            return Ok(MapToDto(taskTimeLog));
        }

        // GET: api/tasktimelogs/task/{taskId}
        [HttpGet("task/{taskId}")]
        public async Task<ActionResult<IEnumerable<TaskTimeLogDto>>> GetTaskTimeLogsByTask(Guid taskId)
        {
            var taskTimeLogs = await _taskTimeLogRepository.GetByTaskIdAsync(taskId);
            var dtos = taskTimeLogs.Select(MapToDto);
            return Ok(dtos);
        }

        // GET: api/tasktimelogs/resource/{resourceId}
        [HttpGet("resource/{resourceId}")]
        public async Task<ActionResult<IEnumerable<TaskTimeLogDto>>> GetTaskTimeLogsByResource(Guid resourceId)
        {
            var taskTimeLogs = await _taskTimeLogRepository.GetByResourceIdAsync(resourceId);
            var dtos = taskTimeLogs.Select(MapToDto);
            return Ok(dtos);
        }

        // GET: api/tasktimelogs/tenant/{tenantId}
        [HttpGet("tenant/{tenantId}")]
        public async Task<ActionResult<IEnumerable<TaskTimeLogDto>>> GetTaskTimeLogsByTenant(Guid tenantId)
        {
            var taskTimeLogs = await _taskTimeLogRepository.GetByTenantIdAsync(tenantId);
            var dtos = taskTimeLogs.Select(MapToDto);
            return Ok(dtos);
        }

        // GET: api/tasktimelogs/daterange?startDate={startDate}&endDate={endDate}
        [HttpGet("daterange")]
        public async Task<ActionResult<IEnumerable<TaskTimeLogDto>>> GetTaskTimeLogsByDateRange(
            [FromQuery] DateOnly startDate, 
            [FromQuery] DateOnly endDate)
        {
            var taskTimeLogs = await _taskTimeLogRepository.GetByDateRangeAsync(startDate, endDate);
            var dtos = taskTimeLogs.Select(MapToDto);
            return Ok(dtos);
        }

        // GET: api/tasktimelogs/task/{taskId}/daterange?startDate={startDate}&endDate={endDate}
        [HttpGet("task/{taskId}/daterange")]
        public async Task<ActionResult<IEnumerable<TaskTimeLogDto>>> GetTaskTimeLogsByTaskAndDateRange(
            Guid taskId,
            [FromQuery] DateOnly startDate,
            [FromQuery] DateOnly endDate)
        {
            var taskTimeLogs = await _taskTimeLogRepository.GetByTaskAndDateRangeAsync(taskId, startDate, endDate);
            var dtos = taskTimeLogs.Select(MapToDto);
            return Ok(dtos);
        }

        // GET: api/tasktimelogs/resource/{resourceId}/daterange?startDate={startDate}&endDate={endDate}
        [HttpGet("resource/{resourceId}/daterange")]
        public async Task<ActionResult<IEnumerable<TaskTimeLogDto>>> GetTaskTimeLogsByResourceAndDateRange(
            Guid resourceId,
            [FromQuery] DateOnly startDate,
            [FromQuery] DateOnly endDate)
        {
            var taskTimeLogs = await _taskTimeLogRepository.GetByResourceAndDateRangeAsync(resourceId, startDate, endDate);
            var dtos = taskTimeLogs.Select(MapToDto);
            return Ok(dtos);
        }

        // GET: api/tasktimelogs/tenant/{tenantId}/daterange?startDate={startDate}&endDate={endDate}
        [HttpGet("tenant/{tenantId}/daterange")]
        public async Task<ActionResult<IEnumerable<TaskTimeLogDto>>> GetTaskTimeLogsByTenantAndDateRange(
            Guid tenantId,
            [FromQuery] DateOnly startDate,
            [FromQuery] DateOnly endDate)
        {
            var taskTimeLogs = await _taskTimeLogRepository.GetByTenantAndDateRangeAsync(tenantId, startDate, endDate);
            var dtos = taskTimeLogs.Select(MapToDto);
            return Ok(dtos);
        }

        // GET: api/tasktimelogs/task/{taskId}/total-hours
        [HttpGet("task/{taskId}/total-hours")]
        public async Task<ActionResult<decimal>> GetTotalHoursByTask(Guid taskId)
        {
            var totalHours = await _taskTimeLogRepository.GetTotalHoursByTaskAsync(taskId);
            return Ok(totalHours);
        }

        // GET: api/tasktimelogs/resource/{resourceId}/total-hours
        [HttpGet("resource/{resourceId}/total-hours")]
        public async Task<ActionResult<decimal>> GetTotalHoursByResource(Guid resourceId)
        {
            var totalHours = await _taskTimeLogRepository.GetTotalHoursByResourceAsync(resourceId);
            return Ok(totalHours);
        }

        // GET: api/tasktimelogs/task/{taskId}/billable-hours
        [HttpGet("task/{taskId}/billable-hours")]
        public async Task<ActionResult<decimal>> GetBillableHoursByTask(Guid taskId)
        {
            var billableHours = await _taskTimeLogRepository.GetTotalBillableHoursByTaskAsync(taskId);
            return Ok(billableHours);
        }

        // GET: api/tasktimelogs/resource/{resourceId}/billable-hours
        [HttpGet("resource/{resourceId}/billable-hours")]
        public async Task<ActionResult<decimal>> GetBillableHoursByResource(Guid resourceId)
        {
            var billableHours = await _taskTimeLogRepository.GetTotalBillableHoursByResourceAsync(resourceId);
            return Ok(billableHours);
        }

        // GET: api/tasktimelogs/task/{taskId}/total-cost
        [HttpGet("task/{taskId}/total-cost")]
        public async Task<ActionResult<decimal>> GetTotalCostByTask(Guid taskId)
        {
            var totalCost = await _taskTimeLogRepository.GetTotalCostByTaskAsync(taskId);
            return Ok(totalCost);
        }

        // GET: api/tasktimelogs/resource/{resourceId}/total-cost
        [HttpGet("resource/{resourceId}/total-cost")]
        public async Task<ActionResult<decimal>> GetTotalCostByResource(Guid resourceId)
        {
            var totalCost = await _taskTimeLogRepository.GetTotalCostByResourceAsync(resourceId);
            return Ok(totalCost);
        }

        // POST: api/tasktimelogs
        [HttpPost]
        public async Task<ActionResult<TaskTimeLogDto>> CreateTaskTimeLog(CreateTaskTimeLogDto createDto)
        {
            // Validate that task exists
            var task = await _taskRepository.GetByIdAsync(createDto.TaskId);
            if (task == null)
            {
                return BadRequest("Task not found");
            }

            // Validate that resource exists
            var resource = await _resourceRepository.GetByIdAsync(createDto.ResourceId);
            if (resource == null)
            {
                return BadRequest("Resource not found");
            }

            var taskTimeLog = new TaskTimeLog
            {
                TaskId = createDto.TaskId,
                ResourceId = createDto.ResourceId,
                DateWorked = createDto.DateWorked,
                HoursWorked = createDto.HoursWorked,
                Description = createDto.Description,
                WorkType = createDto.WorkType,
                IsBillable = createDto.IsBillable,
                HourlyRate = createDto.HourlyRate,
                TenantId = createDto.TenantId
            };

            var createdTaskTimeLog = await _taskTimeLogRepository.CreateAsync(taskTimeLog);
            return CreatedAtAction(nameof(GetTaskTimeLog), new { id = createdTaskTimeLog.TaskTimeLogId }, MapToDto(createdTaskTimeLog));
        }

        // PUT: api/tasktimelogs/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaskTimeLog(Guid id, UpdateTaskTimeLogDto updateDto)
        {
            var existingTaskTimeLog = await _taskTimeLogRepository.GetByIdAsync(id);
            if (existingTaskTimeLog == null)
            {
                return NotFound();
            }

            // Update only the provided fields
            if (updateDto.HoursWorked.HasValue)
                existingTaskTimeLog.HoursWorked = updateDto.HoursWorked.Value;
            
            if (updateDto.Description != null)
                existingTaskTimeLog.Description = updateDto.Description;
            
            if (updateDto.WorkType != null)
                existingTaskTimeLog.WorkType = updateDto.WorkType;
            
            if (updateDto.IsBillable.HasValue)
                existingTaskTimeLog.IsBillable = updateDto.IsBillable.Value;
            
            if (updateDto.HourlyRate.HasValue)
                existingTaskTimeLog.HourlyRate = updateDto.HourlyRate.Value;

            var updatedTaskTimeLog = await _taskTimeLogRepository.UpdateAsync(existingTaskTimeLog);
            return Ok(MapToDto(updatedTaskTimeLog));
        }

        // DELETE: api/tasktimelogs/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskTimeLog(Guid id)
        {
            var success = await _taskTimeLogRepository.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        private static TaskTimeLogDto MapToDto(TaskTimeLog taskTimeLog)
        {
            return new TaskTimeLogDto
            {
                TaskTimeLogId = taskTimeLog.TaskTimeLogId,
                TaskId = taskTimeLog.TaskId,
                ResourceId = taskTimeLog.ResourceId,
                DateWorked = taskTimeLog.DateWorked,
                HoursWorked = taskTimeLog.HoursWorked,
                Description = taskTimeLog.Description,
                WorkType = taskTimeLog.WorkType,
                IsBillable = taskTimeLog.IsBillable,
                HourlyRate = taskTimeLog.HourlyRate,
                CreatedAt = taskTimeLog.CreatedAt,
                UpdatedAt = taskTimeLog.UpdatedAt,
                TenantId = taskTimeLog.TenantId,
                TotalCost = taskTimeLog.TotalCost,
                Task = taskTimeLog.Task != null ? new TaskDto
                {
                    TaskId = taskTimeLog.Task.TaskId,
                    TaskName = taskTimeLog.Task.TaskName,
                    TaskStatus = taskTimeLog.Task.TaskStatus,
                    Priority = taskTimeLog.Task.Priority,
                    Description = taskTimeLog.Task.Description
                } : null,
                Resource = taskTimeLog.Resource != null ? new ResourceDto
                {
                    ResourceId = taskTimeLog.Resource.ResourceId,
                    FirstName = taskTimeLog.Resource.FirstName,
                    LastName = taskTimeLog.Resource.LastName,
                    Email = taskTimeLog.Resource.Email,
                    RoleTitle = taskTimeLog.Resource.RoleTitle,
                    Department = taskTimeLog.Resource.Department,
                    Skills = taskTimeLog.Resource.Skills,
                    ExperienceLevel = taskTimeLog.Resource.ExperienceLevel,
                    EmploymentStatus = taskTimeLog.Resource.EmploymentStatus,
                    CreatedAt = taskTimeLog.Resource.CreatedAt,
                    UpdatedAt = taskTimeLog.Resource.UpdatedAt,
                    UserId = taskTimeLog.Resource.UserId,
                    TenantId = taskTimeLog.Resource.TenantId
                } : null,
                Tenant = taskTimeLog.Tenant != null ? new OrganizationDto
                {
                    OrganizationId = taskTimeLog.Tenant.OrganizationId,
                    OrganizationName = taskTimeLog.Tenant.OrganizationName,
                    Status = taskTimeLog.Tenant.Status,
                    BillingPlan = taskTimeLog.Tenant.BillingPlan,
                    ContactEmail = taskTimeLog.Tenant.ContactEmail,
                    CreatedAt = taskTimeLog.Tenant.CreatedAt,
                    UpdatedAt = taskTimeLog.Tenant.UpdatedAt
                } : null
            };
        }
    }
} 