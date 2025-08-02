using Microsoft.AspNetCore.Mvc;
using MyProject.Persistence.Entities;
using MyProject.Persistence.Repositories;
using MyProject.WebAPI.DTOs;

namespace MyProject.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectsController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjects()
        {
            var projects = await _projectRepository.GetAllAsync();
            var projectDtos = projects.Select(MapToDto);
            return Ok(projectDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> GetProject(Guid id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(MapToDto(project));
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjectsByStatus(string status)
        {
            var projects = await _projectRepository.GetByStatusAsync(status);
            var projectDtos = projects.Select(MapToDto);
            return Ok(projectDtos);
        }

        [HttpGet("manager/{managerId}")]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjectsByManager(Guid managerId)
        {
            var projects = await _projectRepository.GetByManagerIdAsync(managerId);
            var projectDtos = projects.Select(MapToDto);
            return Ok(projectDtos);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectDto>> CreateProject(CreateProjectDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var project = MapToEntity(createDto);
            var createdProject = await _projectRepository.AddAsync(project);
            return CreatedAtAction(nameof(GetProject), new { id = createdProject.ProjectId }, MapToDto(createdProject));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(Guid id, UpdateProjectDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingProject = await _projectRepository.GetByIdAsync(id);
            if (existingProject == null)
            {
                return NotFound();
            }

            UpdateEntityFromDto(existingProject, updateDto);
            var updatedProject = await _projectRepository.UpdateAsync(existingProject);
            return Ok(MapToDto(updatedProject));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            var deleted = await _projectRepository.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        private static ProjectDto MapToDto(Project project)
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

        private static Project MapToEntity(CreateProjectDto dto)
        {
            return new Project
            {
                ProjectName = dto.ProjectName,
                ProjectType = dto.ProjectType,
                IndustryDomain = dto.IndustryDomain,
                PlannedStartDate = dto.PlannedStartDate,
                PlannedEndDate = dto.PlannedEndDate,
                ActualStartDate = dto.ActualStartDate,
                ActualEndDate = dto.ActualEndDate,
                PlannedBudget = dto.PlannedBudget,
                ActualBudget = dto.ActualBudget,
                ProjectManagerId = dto.ProjectManagerId,
                TeamSize = dto.TeamSize,
                ContractType = dto.ContractType,
                ProjectStatus = dto.ProjectStatus,
                ProjectComplexity = dto.ProjectComplexity,
                Description = dto.Description,
                Notes = dto.Notes,
                CurrentPhase = dto.CurrentPhase,
                OrganizationId = dto.OrganizationId
            };
        }

        private static void UpdateEntityFromDto(Project project, UpdateProjectDto dto)
        {
            if (dto.ProjectName != null)
                project.ProjectName = dto.ProjectName;
            if (dto.ProjectType != null)
                project.ProjectType = dto.ProjectType;
            if (dto.IndustryDomain != null)
                project.IndustryDomain = dto.IndustryDomain;
            if (dto.PlannedStartDate.HasValue)
                project.PlannedStartDate = dto.PlannedStartDate;
            if (dto.PlannedEndDate.HasValue)
                project.PlannedEndDate = dto.PlannedEndDate;
            if (dto.ActualStartDate.HasValue)
                project.ActualStartDate = dto.ActualStartDate;
            if (dto.ActualEndDate.HasValue)
                project.ActualEndDate = dto.ActualEndDate;
            if (dto.PlannedBudget.HasValue)
                project.PlannedBudget = dto.PlannedBudget;
            if (dto.ActualBudget.HasValue)
                project.ActualBudget = dto.ActualBudget;
            if (dto.ProjectManagerId.HasValue)
                project.ProjectManagerId = dto.ProjectManagerId;
            if (dto.TeamSize.HasValue)
                project.TeamSize = dto.TeamSize;
            if (dto.ContractType != null)
                project.ContractType = dto.ContractType;
            if (dto.ProjectStatus != null)
                project.ProjectStatus = dto.ProjectStatus;
            if (dto.ProjectComplexity != null)
                project.ProjectComplexity = dto.ProjectComplexity;
            if (dto.Description != null)
                project.Description = dto.Description;
            if (dto.Notes != null)
                project.Notes = dto.Notes;
            if (dto.CurrentPhase != null)
                project.CurrentPhase = dto.CurrentPhase;
            if (dto.OrganizationId.HasValue)
                project.OrganizationId = dto.OrganizationId;
        }
    }
} 