using Microsoft.AspNetCore.Mvc;
using MyProject.Persistence.Entities;
using MyProject.Persistence.Repositories;
using MyProject.WebAPI.DTOs;

namespace MyProject.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MilestonesController : ControllerBase
    {
        private readonly IMilestoneRepository _milestoneRepository;

        public MilestonesController(IMilestoneRepository milestoneRepository)
        {
            _milestoneRepository = milestoneRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MilestoneDto>>> GetMilestones()
        {
            var milestones = await _milestoneRepository.GetAllAsync();
            var milestoneDtos = milestones.Select(MapToDto);
            return Ok(milestoneDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MilestoneDto>> GetMilestone(Guid id)
        {
            var milestone = await _milestoneRepository.GetByIdAsync(id);
            if (milestone == null)
            {
                return NotFound();
            }
            return Ok(MapToDto(milestone));
        }

        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<IEnumerable<MilestoneDto>>> GetMilestonesByProject(Guid projectId)
        {
            var milestones = await _milestoneRepository.GetByProjectIdAsync(projectId);
            var milestoneDtos = milestones.Select(MapToDto);
            return Ok(milestoneDtos);
        }

        [HttpGet("tenant/{tenantId}")]
        public async Task<ActionResult<IEnumerable<MilestoneDto>>> GetMilestonesByTenant(Guid tenantId)
        {
            var milestones = await _milestoneRepository.GetByTenantIdAsync(tenantId);
            var milestoneDtos = milestones.Select(MapToDto);
            return Ok(milestoneDtos);
        }

        [HttpGet("achieved")]
        public async Task<ActionResult<IEnumerable<MilestoneDto>>> GetAchievedMilestones()
        {
            var milestones = await _milestoneRepository.GetAchievedMilestonesAsync();
            var milestoneDtos = milestones.Select(MapToDto);
            return Ok(milestoneDtos);
        }

        [HttpGet("pending")]
        public async Task<ActionResult<IEnumerable<MilestoneDto>>> GetPendingMilestones()
        {
            var milestones = await _milestoneRepository.GetPendingMilestonesAsync();
            var milestoneDtos = milestones.Select(MapToDto);
            return Ok(milestoneDtos);
        }

        [HttpGet("overdue")]
        public async Task<ActionResult<IEnumerable<MilestoneDto>>> GetOverdueMilestones()
        {
            var milestones = await _milestoneRepository.GetOverdueMilestonesAsync();
            var milestoneDtos = milestones.Select(MapToDto);
            return Ok(milestoneDtos);
        }

        [HttpPost]
        public async Task<ActionResult<MilestoneDto>> CreateMilestone(CreateMilestoneDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if milestone name already exists in the project
            if (await _milestoneRepository.ExistsByNameInProjectAsync(createDto.ProjectId, createDto.MilestoneName))
            {
                return BadRequest("Milestone with this name already exists in the project.");
            }

            var milestone = MapToEntity(createDto);
            var createdMilestone = await _milestoneRepository.AddAsync(milestone);
            return CreatedAtAction(nameof(GetMilestone), new { id = createdMilestone.MilestoneId }, MapToDto(createdMilestone));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMilestone(Guid id, UpdateMilestoneDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingMilestone = await _milestoneRepository.GetByIdAsync(id);
            if (existingMilestone == null)
            {
                return NotFound();
            }

            // Check if the new name conflicts with another milestone in the same project
            if (!string.IsNullOrEmpty(updateDto.MilestoneName) && 
                updateDto.MilestoneName != existingMilestone.MilestoneName)
            {
                if (await _milestoneRepository.ExistsByNameInProjectAsync(existingMilestone.ProjectId, updateDto.MilestoneName, id))
                {
                    return BadRequest("Milestone with this name already exists in the project.");
                }
            }

            UpdateEntityFromDto(existingMilestone, updateDto);
            var updatedMilestone = await _milestoneRepository.UpdateAsync(existingMilestone);
            return Ok(MapToDto(updatedMilestone));
        }

        [HttpPut("{id}/mark-achieved")]
        public async Task<IActionResult> MarkMilestoneAchieved(Guid id, MarkMilestoneAchievedDto markDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingMilestone = await _milestoneRepository.GetByIdAsync(id);
            if (existingMilestone == null)
            {
                return NotFound();
            }

            existingMilestone.IsAchieved = true;
            existingMilestone.CompletionDate = markDto.CompletionDate ?? DateOnly.FromDateTime(DateTime.Today);
            existingMilestone.UpdatedAt = DateTime.UtcNow;

            var updatedMilestone = await _milestoneRepository.UpdateAsync(existingMilestone);
            return Ok(MapToDto(updatedMilestone));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMilestone(Guid id)
        {
            var deleted = await _milestoneRepository.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        private static MilestoneDto MapToDto(Milestone milestone)
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

        private static Milestone MapToEntity(CreateMilestoneDto dto)
        {
            return new Milestone
            {
                ProjectId = dto.ProjectId,
                MilestoneName = dto.MilestoneName,
                Description = dto.Description,
                DueDate = dto.DueDate,
                IsAchieved = dto.IsAchieved,
                CompletionDate = dto.CompletionDate,
                TenantId = dto.TenantId
            };
        }

        private static void UpdateEntityFromDto(Milestone milestone, UpdateMilestoneDto dto)
        {
            if (!string.IsNullOrEmpty(dto.MilestoneName))
                milestone.MilestoneName = dto.MilestoneName;

            if (dto.Description != null)
                milestone.Description = dto.Description;

            if (dto.DueDate.HasValue)
                milestone.DueDate = dto.DueDate.Value;

            if (dto.IsAchieved.HasValue)
                milestone.IsAchieved = dto.IsAchieved.Value;

            if (dto.CompletionDate.HasValue)
                milestone.CompletionDate = dto.CompletionDate.Value;
        }
    }
} 