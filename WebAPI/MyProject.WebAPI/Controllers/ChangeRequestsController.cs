using Microsoft.AspNetCore.Mvc;
using MyProject.Persistence.Entities;
using MyProject.Persistence.Repositories;
using MyProject.WebAPI.DTOs;

namespace MyProject.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChangeRequestsController : ControllerBase
    {
        private readonly IChangeRequestRepository _changeRequestRepository;

        public ChangeRequestsController(IChangeRequestRepository changeRequestRepository)
        {
            _changeRequestRepository = changeRequestRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChangeRequestDto>>> GetChangeRequests()
        {
            var changeRequests = await _changeRequestRepository.GetAllAsync();
            var changeRequestDtos = changeRequests.Select(MapToDto);
            return Ok(changeRequestDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChangeRequestDto>> GetChangeRequest(Guid id)
        {
            var changeRequest = await _changeRequestRepository.GetByIdAsync(id);
            if (changeRequest == null)
            {
                return NotFound();
            }
            return Ok(MapToDto(changeRequest));
        }

        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<IEnumerable<ChangeRequestDto>>> GetChangeRequestsByProject(Guid projectId)
        {
            var changeRequests = await _changeRequestRepository.GetByProjectIdAsync(projectId);
            var changeRequestDtos = changeRequests.Select(MapToDto);
            return Ok(changeRequestDtos);
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<ChangeRequestDto>>> GetChangeRequestsByStatus(string status)
        {
            var changeRequests = await _changeRequestRepository.GetByStatusAsync(status);
            var changeRequestDtos = changeRequests.Select(MapToDto);
            return Ok(changeRequestDtos);
        }

        [HttpGet("requested-by/{userId}")]
        public async Task<ActionResult<IEnumerable<ChangeRequestDto>>> GetChangeRequestsByRequestor(Guid userId)
        {
            var changeRequests = await _changeRequestRepository.GetByRequestedByUserIdAsync(userId);
            var changeRequestDtos = changeRequests.Select(MapToDto);
            return Ok(changeRequestDtos);
        }

        [HttpGet("approved-by/{userId}")]
        public async Task<ActionResult<IEnumerable<ChangeRequestDto>>> GetChangeRequestsByApprover(Guid userId)
        {
            var changeRequests = await _changeRequestRepository.GetByApprovedByUserIdAsync(userId);
            var changeRequestDtos = changeRequests.Select(MapToDto);
            return Ok(changeRequestDtos);
        }

        [HttpGet("pending")]
        public async Task<ActionResult<IEnumerable<ChangeRequestDto>>> GetPendingChangeRequests()
        {
            var changeRequests = await _changeRequestRepository.GetPendingRequestsAsync();
            var changeRequestDtos = changeRequests.Select(MapToDto);
            return Ok(changeRequestDtos);
        }

        [HttpGet("approved")]
        public async Task<ActionResult<IEnumerable<ChangeRequestDto>>> GetApprovedChangeRequests()
        {
            var changeRequests = await _changeRequestRepository.GetApprovedRequestsAsync();
            var changeRequestDtos = changeRequests.Select(MapToDto);
            return Ok(changeRequestDtos);
        }

        [HttpGet("rejected")]
        public async Task<ActionResult<IEnumerable<ChangeRequestDto>>> GetRejectedChangeRequests()
        {
            var changeRequests = await _changeRequestRepository.GetRejectedRequestsAsync();
            var changeRequestDtos = changeRequests.Select(MapToDto);
            return Ok(changeRequestDtos);
        }

        [HttpGet("date-range")]
        public async Task<ActionResult<IEnumerable<ChangeRequestDto>>> GetChangeRequestsByDateRange(
            [FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate)
        {
            var changeRequests = await _changeRequestRepository.GetByRequestDateRangeAsync(startDate, endDate);
            var changeRequestDtos = changeRequests.Select(MapToDto);
            return Ok(changeRequestDtos);
        }

        [HttpGet("version/{version}")]
        public async Task<ActionResult<IEnumerable<ChangeRequestDto>>> GetChangeRequestsByVersion(string version)
        {
            var changeRequests = await _changeRequestRepository.GetByVersionAffectedAsync(version);
            var changeRequestDtos = changeRequests.Select(MapToDto);
            return Ok(changeRequestDtos);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ChangeRequestDto>>> SearchChangeRequests([FromQuery] string searchTerm)
        {
            var changeRequests = await _changeRequestRepository.SearchByTitleAsync(searchTerm);
            var changeRequestDtos = changeRequests.Select(MapToDto);
            return Ok(changeRequestDtos);
        }

        [HttpGet("tenant/{tenantId}")]
        public async Task<ActionResult<IEnumerable<ChangeRequestDto>>> GetChangeRequestsByTenant(Guid tenantId)
        {
            var changeRequests = await _changeRequestRepository.GetByTenantIdAsync(tenantId);
            var changeRequestDtos = changeRequests.Select(MapToDto);
            return Ok(changeRequestDtos);
        }

        [HttpGet("milestone/{milestoneId}")]
        public async Task<ActionResult<IEnumerable<ChangeRequestDto>>> GetChangeRequestsByMilestone(Guid milestoneId)
        {
            var changeRequests = await _changeRequestRepository.GetByMilestoneIdAsync(milestoneId);
            var changeRequestDtos = changeRequests.Select(MapToDto);
            return Ok(changeRequestDtos);
        }

        [HttpGet("tenant/{tenantId}/status/{status}")]
        public async Task<ActionResult<IEnumerable<ChangeRequestDto>>> GetChangeRequestsByTenantAndStatus(Guid tenantId, string status)
        {
            var changeRequests = await _changeRequestRepository.GetByTenantAndStatusAsync(tenantId, status);
            var changeRequestDtos = changeRequests.Select(MapToDto);
            return Ok(changeRequestDtos);
        }

        [HttpGet("project/{projectId}/status/{status}")]
        public async Task<ActionResult<IEnumerable<ChangeRequestDto>>> GetChangeRequestsByProjectAndStatus(Guid projectId, string status)
        {
            var changeRequests = await _changeRequestRepository.GetByProjectAndStatusAsync(projectId, status);
            var changeRequestDtos = changeRequests.Select(MapToDto);
            return Ok(changeRequestDtos);
        }

        [HttpPost]
        public async Task<ActionResult<ChangeRequestDto>> CreateChangeRequest(CreateChangeRequestDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var changeRequest = MapToEntity(createDto);
            var createdChangeRequest = await _changeRequestRepository.AddAsync(changeRequest);
            return CreatedAtAction(nameof(GetChangeRequest), new { id = createdChangeRequest.ChangeRequestId }, MapToDto(createdChangeRequest));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChangeRequest(Guid id, UpdateChangeRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingChangeRequest = await _changeRequestRepository.GetByIdAsync(id);
            if (existingChangeRequest == null)
            {
                return NotFound();
            }

            UpdateEntityFromDto(existingChangeRequest, updateDto);
            var updatedChangeRequest = await _changeRequestRepository.UpdateAsync(existingChangeRequest);
            return Ok(MapToDto(updatedChangeRequest));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChangeRequest(Guid id)
        {
            var deleted = await _changeRequestRepository.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        private static ChangeRequestDto MapToDto(ChangeRequest changeRequest)
        {
            return new ChangeRequestDto
            {
                ChangeRequestId = changeRequest.ChangeRequestId,
                ProjectId = changeRequest.ProjectId,
                RequestTitle = changeRequest.RequestTitle,
                RequestDescription = changeRequest.RequestDescription,
                ReasonForChange = changeRequest.ReasonForChange,
                EstimatedImpactOnScope = changeRequest.EstimatedImpactOnScope,
                EstimatedImpactOnScheduleDays = changeRequest.EstimatedImpactOnScheduleDays,
                EstimatedImpactOnBudgetUsd = changeRequest.EstimatedImpactOnBudgetUsd,
                EstimatedImpactOnQuality = changeRequest.EstimatedImpactOnQuality,
                RequestedByUserId = changeRequest.RequestedByUserId,
                RequestDate = changeRequest.RequestDate,
                ApprovalStatus = changeRequest.ApprovalStatus,
                ApprovedByUserId = changeRequest.ApprovedByUserId,
                ApprovalDate = changeRequest.ApprovalDate,
                ApprovalNotes = changeRequest.ApprovalNotes,
                VersionAffected = changeRequest.VersionAffected,
                ActualImpactOnScheduleDays = changeRequest.ActualImpactOnScheduleDays,
                ActualImpactOnBudgetUsd = changeRequest.ActualImpactOnBudgetUsd,
                CreatedAt = changeRequest.CreatedAt,
                UpdatedAt = changeRequest.UpdatedAt,
                TenantId = changeRequest.TenantId,
                MilestoneId = changeRequest.MilestoneId,
                Project = changeRequest.Project != null ? MapProjectToDto(changeRequest.Project) : null,
                RequestedByUser = changeRequest.RequestedByUser != null ? MapUserToDto(changeRequest.RequestedByUser) : null,
                ApprovedByUser = changeRequest.ApprovedByUser != null ? MapUserToDto(changeRequest.ApprovedByUser) : null,
                Tenant = changeRequest.Tenant != null ? MapOrganizationToDto(changeRequest.Tenant) : null,
                Milestone = changeRequest.Milestone != null ? MapMilestoneToDto(changeRequest.Milestone) : null
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
                DueDate = milestone.DueDate,
                IsAchieved = milestone.IsAchieved,
                CompletionDate = milestone.CompletionDate,
                TenantId = milestone.TenantId,
                CreatedAt = milestone.CreatedAt,
                UpdatedAt = milestone.UpdatedAt
            };
        }

        private static ChangeRequest MapToEntity(CreateChangeRequestDto dto)
        {
            return new ChangeRequest
            {
                ProjectId = dto.ProjectId,
                RequestTitle = dto.RequestTitle,
                RequestDescription = dto.RequestDescription,
                ReasonForChange = dto.ReasonForChange,
                EstimatedImpactOnScope = dto.EstimatedImpactOnScope,
                EstimatedImpactOnScheduleDays = dto.EstimatedImpactOnScheduleDays,
                EstimatedImpactOnBudgetUsd = dto.EstimatedImpactOnBudgetUsd,
                EstimatedImpactOnQuality = dto.EstimatedImpactOnQuality,
                RequestedByUserId = dto.RequestedByUserId,
                RequestDate = dto.RequestDate,
                ApprovalStatus = dto.ApprovalStatus,
                ApprovedByUserId = dto.ApprovedByUserId,
                ApprovalDate = dto.ApprovalDate,
                ApprovalNotes = dto.ApprovalNotes,
                VersionAffected = dto.VersionAffected,
                ActualImpactOnScheduleDays = dto.ActualImpactOnScheduleDays,
                ActualImpactOnBudgetUsd = dto.ActualImpactOnBudgetUsd,
                TenantId = dto.TenantId,
                MilestoneId = dto.MilestoneId
            };
        }

        private static void UpdateEntityFromDto(ChangeRequest changeRequest, UpdateChangeRequestDto dto)
        {
            if (dto.RequestTitle != null)
                changeRequest.RequestTitle = dto.RequestTitle;
            if (dto.RequestDescription != null)
                changeRequest.RequestDescription = dto.RequestDescription;
            if (dto.ReasonForChange != null)
                changeRequest.ReasonForChange = dto.ReasonForChange;
            if (dto.EstimatedImpactOnScope != null)
                changeRequest.EstimatedImpactOnScope = dto.EstimatedImpactOnScope;
            if (dto.EstimatedImpactOnScheduleDays.HasValue)
                changeRequest.EstimatedImpactOnScheduleDays = dto.EstimatedImpactOnScheduleDays;
            if (dto.EstimatedImpactOnBudgetUsd.HasValue)
                changeRequest.EstimatedImpactOnBudgetUsd = dto.EstimatedImpactOnBudgetUsd;
            if (dto.EstimatedImpactOnQuality != null)
                changeRequest.EstimatedImpactOnQuality = dto.EstimatedImpactOnQuality;
            if (dto.ApprovalStatus != null)
                changeRequest.ApprovalStatus = dto.ApprovalStatus;
            if (dto.ApprovedByUserId.HasValue)
                changeRequest.ApprovedByUserId = dto.ApprovedByUserId;
            if (dto.ApprovalDate.HasValue)
                changeRequest.ApprovalDate = dto.ApprovalDate;
            if (dto.ApprovalNotes != null)
                changeRequest.ApprovalNotes = dto.ApprovalNotes;
            if (dto.VersionAffected != null)
                changeRequest.VersionAffected = dto.VersionAffected;
            if (dto.ActualImpactOnScheduleDays.HasValue)
                changeRequest.ActualImpactOnScheduleDays = dto.ActualImpactOnScheduleDays;
            if (dto.ActualImpactOnBudgetUsd.HasValue)
                changeRequest.ActualImpactOnBudgetUsd = dto.ActualImpactOnBudgetUsd;
            if (dto.MilestoneId.HasValue)
                changeRequest.MilestoneId = dto.MilestoneId;
        }
    }
} 