using Microsoft.AspNetCore.Mvc;
using MyProject.Persistence.Entities;
using MyProject.Persistence.Repositories;
using MyProject.WebAPI.DTOs;

namespace MyProject.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RisksController : ControllerBase
    {
        private readonly IRiskRepository _riskRepository;

        public RisksController(IRiskRepository riskRepository)
        {
            _riskRepository = riskRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RiskDto>>> GetRisks()
        {
            var risks = await _riskRepository.GetAllAsync();
            var riskDtos = risks.Select(MapToDto);
            return Ok(riskDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RiskDto>> GetRisk(Guid id)
        {
            var risk = await _riskRepository.GetByIdAsync(id);
            if (risk == null)
            {
                return NotFound();
            }
            return Ok(MapToDto(risk));
        }

        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<IEnumerable<RiskDto>>> GetRisksByProject(Guid projectId)
        {
            var risks = await _riskRepository.GetByProjectIdAsync(projectId);
            var riskDtos = risks.Select(MapToDto);
            return Ok(riskDtos);
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<RiskDto>>> GetRisksByStatus(string status)
        {
            var risks = await _riskRepository.GetByStatusAsync(status);
            var riskDtos = risks.Select(MapToDto);
            return Ok(riskDtos);
        }

        [HttpGet("category/{category}")]
        public async Task<ActionResult<IEnumerable<RiskDto>>> GetRisksByCategory(string category)
        {
            var risks = await _riskRepository.GetByCategoryAsync(category);
            var riskDtos = risks.Select(MapToDto);
            return Ok(riskDtos);
        }

        [HttpGet("owner/{ownerId}")]
        public async Task<ActionResult<IEnumerable<RiskDto>>> GetRisksByOwner(Guid ownerId)
        {
            var risks = await _riskRepository.GetByOwnerIdAsync(ownerId);
            var riskDtos = risks.Select(MapToDto);
            return Ok(riskDtos);
        }

        [HttpGet("high-risk")]
        public async Task<ActionResult<IEnumerable<RiskDto>>> GetHighRiskRisks()
        {
            var risks = await _riskRepository.GetHighRiskRisksAsync();
            var riskDtos = risks.Select(MapToDto);
            return Ok(riskDtos);
        }

        [HttpPost]
        public async Task<ActionResult<RiskDto>> CreateRisk(CreateRiskDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var risk = MapToEntity(createDto);
            var createdRisk = await _riskRepository.AddAsync(risk);
            return CreatedAtAction(nameof(GetRisk), new { id = createdRisk.RiskId }, MapToDto(createdRisk));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRisk(Guid id, UpdateRiskDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingRisk = await _riskRepository.GetByIdAsync(id);
            if (existingRisk == null)
            {
                return NotFound();
            }

            UpdateEntityFromDto(existingRisk, updateDto);
            var updatedRisk = await _riskRepository.UpdateAsync(existingRisk);
            return Ok(MapToDto(updatedRisk));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRisk(Guid id)
        {
            var deleted = await _riskRepository.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        private static RiskDto MapToDto(Risk risk)
        {
            return new RiskDto
            {
                RiskId = risk.RiskId,
                ProjectId = risk.ProjectId,
                RiskDescription = risk.RiskDescription,
                RiskCategory = risk.RiskCategory,
                PlannedProbabilityScore = risk.PlannedProbabilityScore,
                ActualProbabilityScore = risk.ActualProbabilityScore,
                PlannedImpactScore = risk.PlannedImpactScore,
                ActualImpactScore = risk.ActualImpactScore,
                PlannedRiskExposure = risk.PlannedRiskExposure,
                ActualRiskExposure = risk.ActualRiskExposure,
                MitigationStrategy = risk.MitigationStrategy,
                ContingencyPlan = risk.ContingencyPlan,
                TriggerEvents = risk.TriggerEvents,
                RiskOwnerId = risk.RiskOwnerId,
                RiskStatus = risk.RiskStatus,
                IdentifiedDate = risk.IdentifiedDate,
                LastReviewDate = risk.LastReviewDate,
                ClosureDate = risk.ClosureDate,
                OutcomeDescription = risk.OutcomeDescription,
                ActualImpactOnScheduleDays = risk.ActualImpactOnScheduleDays,
                ActualImpactOnBudgetUsd = risk.ActualImpactOnBudgetUsd,
                LessonsLearned = risk.LessonsLearned,
                CreatedAt = risk.CreatedAt,
                UpdatedAt = risk.UpdatedAt,
                Project = risk.Project != null ? MapProjectToDto(risk.Project) : null
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

        private static Risk MapToEntity(CreateRiskDto dto)
        {
            return new Risk
            {
                ProjectId = dto.ProjectId,
                RiskDescription = dto.RiskDescription,
                RiskCategory = dto.RiskCategory,
                PlannedProbabilityScore = dto.PlannedProbabilityScore,
                ActualProbabilityScore = dto.ActualProbabilityScore,
                PlannedImpactScore = dto.PlannedImpactScore,
                ActualImpactScore = dto.ActualImpactScore,
                MitigationStrategy = dto.MitigationStrategy,
                ContingencyPlan = dto.ContingencyPlan,
                TriggerEvents = dto.TriggerEvents,
                RiskOwnerId = dto.RiskOwnerId,
                RiskStatus = dto.RiskStatus,
                IdentifiedDate = dto.IdentifiedDate ?? DateOnly.FromDateTime(DateTime.Today),
                LastReviewDate = dto.LastReviewDate,
                ClosureDate = dto.ClosureDate,
                OutcomeDescription = dto.OutcomeDescription,
                ActualImpactOnScheduleDays = dto.ActualImpactOnScheduleDays,
                ActualImpactOnBudgetUsd = dto.ActualImpactOnBudgetUsd,
                LessonsLearned = dto.LessonsLearned
            };
        }

        private static void UpdateEntityFromDto(Risk risk, UpdateRiskDto dto)
        {
            if (dto.RiskDescription != null)
                risk.RiskDescription = dto.RiskDescription;
            if (dto.RiskCategory != null)
                risk.RiskCategory = dto.RiskCategory;
            if (dto.PlannedProbabilityScore.HasValue)
                risk.PlannedProbabilityScore = dto.PlannedProbabilityScore;
            if (dto.ActualProbabilityScore.HasValue)
                risk.ActualProbabilityScore = dto.ActualProbabilityScore;
            if (dto.PlannedImpactScore.HasValue)
                risk.PlannedImpactScore = dto.PlannedImpactScore;
            if (dto.ActualImpactScore.HasValue)
                risk.ActualImpactScore = dto.ActualImpactScore;
            if (dto.MitigationStrategy != null)
                risk.MitigationStrategy = dto.MitigationStrategy;
            if (dto.ContingencyPlan != null)
                risk.ContingencyPlan = dto.ContingencyPlan;
            if (dto.TriggerEvents != null)
                risk.TriggerEvents = dto.TriggerEvents;
            if (dto.RiskOwnerId.HasValue)
                risk.RiskOwnerId = dto.RiskOwnerId;
            if (dto.RiskStatus != null)
                risk.RiskStatus = dto.RiskStatus;
            if (dto.IdentifiedDate.HasValue)
                risk.IdentifiedDate = dto.IdentifiedDate.Value;
            if (dto.LastReviewDate.HasValue)
                risk.LastReviewDate = dto.LastReviewDate;
            if (dto.ClosureDate.HasValue)
                risk.ClosureDate = dto.ClosureDate;
            if (dto.OutcomeDescription != null)
                risk.OutcomeDescription = dto.OutcomeDescription;
            if (dto.ActualImpactOnScheduleDays.HasValue)
                risk.ActualImpactOnScheduleDays = dto.ActualImpactOnScheduleDays;
            if (dto.ActualImpactOnBudgetUsd.HasValue)
                risk.ActualImpactOnBudgetUsd = dto.ActualImpactOnBudgetUsd;
            if (dto.LessonsLearned != null)
                risk.LessonsLearned = dto.LessonsLearned;
        }
    }
} 