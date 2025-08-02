using Microsoft.AspNetCore.Mvc;
using MyProject.Persistence.Entities;
using MyProject.Persistence.Repositories;
using MyProject.WebAPI.DTOs;

namespace MyProject.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResourcesController : ControllerBase
    {
        private readonly IResourceRepository _resourceRepository;

        public ResourcesController(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResourceDto>>> GetResources()
        {
            var resources = await _resourceRepository.GetAllAsync();
            var resourceDtos = resources.Select(MapToDto);
            return Ok(resourceDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResourceDto>> GetResource(Guid id)
        {
            var resource = await _resourceRepository.GetByIdAsync(id);
            if (resource == null)
            {
                return NotFound();
            }
            return Ok(MapToDto(resource));
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<ResourceDto>> GetResourceByEmail(string email)
        {
            var resource = await _resourceRepository.GetByEmailAsync(email);
            if (resource == null)
            {
                return NotFound();
            }
            return Ok(MapToDto(resource));
        }

        [HttpGet("department/{department}")]
        public async Task<ActionResult<IEnumerable<ResourceDto>>> GetResourcesByDepartment(string department)
        {
            var resources = await _resourceRepository.GetByDepartmentAsync(department);
            var resourceDtos = resources.Select(MapToDto);
            return Ok(resourceDtos);
        }

        [HttpGet("role/{roleTitle}")]
        public async Task<ActionResult<IEnumerable<ResourceDto>>> GetResourcesByRole(string roleTitle)
        {
            var resources = await _resourceRepository.GetByRoleTitleAsync(roleTitle);
            var resourceDtos = resources.Select(MapToDto);
            return Ok(resourceDtos);
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<ResourceDto>>> GetResourcesByStatus(string status)
        {
            var resources = await _resourceRepository.GetByEmploymentStatusAsync(status);
            var resourceDtos = resources.Select(MapToDto);
            return Ok(resourceDtos);
        }

        [HttpGet("location/{location}")]
        public async Task<ActionResult<IEnumerable<ResourceDto>>> GetResourcesByLocation(string location)
        {
            var resources = await _resourceRepository.GetByLocationAsync(location);
            var resourceDtos = resources.Select(MapToDto);
            return Ok(resourceDtos);
        }

        [HttpGet("skills")]
        public async Task<ActionResult<IEnumerable<ResourceDto>>> GetResourcesBySkills([FromQuery] string[] skills)
        {
            var resources = await _resourceRepository.GetBySkillsAsync(skills);
            var resourceDtos = resources.Select(MapToDto);
            return Ok(resourceDtos);
        }

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<ResourceDto>>> GetActiveResources()
        {
            var resources = await _resourceRepository.GetActiveResourcesAsync();
            var resourceDtos = resources.Select(MapToDto);
            return Ok(resourceDtos);
        }

        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<ResourceDto>>> GetAvailableResources()
        {
            var resources = await _resourceRepository.GetAvailableResourcesAsync();
            var resourceDtos = resources.Select(MapToDto);
            return Ok(resourceDtos);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ResourceDto>>> SearchResources([FromQuery] string searchTerm)
        {
            var resources = await _resourceRepository.SearchByNameAsync(searchTerm);
            var resourceDtos = resources.Select(MapToDto);
            return Ok(resourceDtos);
        }

        [HttpPost]
        public async Task<ActionResult<ResourceDto>> CreateResource(CreateResourceDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if email already exists
            if (!string.IsNullOrEmpty(createDto.Email))
            {
                var existingResource = await _resourceRepository.GetByEmailAsync(createDto.Email);
                if (existingResource != null)
                {
                    return BadRequest("Email already exists");
                }
            }

            var resource = MapToEntity(createDto);
            var createdResource = await _resourceRepository.AddAsync(resource);
            return CreatedAtAction(nameof(GetResource), new { id = createdResource.ResourceId }, MapToDto(createdResource));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResource(Guid id, UpdateResourceDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingResource = await _resourceRepository.GetByIdAsync(id);
            if (existingResource == null)
            {
                return NotFound();
            }

            // Check if email is being changed and if it already exists
            if (!string.IsNullOrEmpty(updateDto.Email) && updateDto.Email != existingResource.Email)
            {
                var resourceWithEmail = await _resourceRepository.GetByEmailAsync(updateDto.Email);
                if (resourceWithEmail != null)
                {
                    return BadRequest("Email already exists");
                }
            }

            UpdateEntityFromDto(existingResource, updateDto);
            var updatedResource = await _resourceRepository.UpdateAsync(existingResource);
            return Ok(MapToDto(updatedResource));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResource(Guid id)
        {
            var deleted = await _resourceRepository.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        private static ResourceDto MapToDto(Resource resource)
        {
            return new ResourceDto
            {
                ResourceId = resource.ResourceId,
                FirstName = resource.FirstName,
                LastName = resource.LastName,
                Email = resource.Email,
                RoleTitle = resource.RoleTitle,
                Department = resource.Department,
                Skills = resource.Skills,
                ExperienceLevel = resource.ExperienceLevel,
                FullTimeEquivalent = resource.FullTimeEquivalent,
                WeeklyCapacityHours = resource.WeeklyCapacityHours,
                CostRatePerHour = resource.CostRatePerHour,
                Location = resource.Location,
                EmploymentStatus = resource.EmploymentStatus,
                StartDateEmployment = resource.StartDateEmployment,
                EndDateEmployment = resource.EndDateEmployment,
                CreatedAt = resource.CreatedAt,
                UpdatedAt = resource.UpdatedAt,
                FullName = resource.FullName
            };
        }

        private static Resource MapToEntity(CreateResourceDto dto)
        {
            return new Resource
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                RoleTitle = dto.RoleTitle,
                Department = dto.Department,
                Skills = dto.Skills,
                ExperienceLevel = dto.ExperienceLevel,
                FullTimeEquivalent = dto.FullTimeEquivalent,
                WeeklyCapacityHours = dto.WeeklyCapacityHours,
                CostRatePerHour = dto.CostRatePerHour,
                Location = dto.Location,
                EmploymentStatus = dto.EmploymentStatus,
                StartDateEmployment = dto.StartDateEmployment,
                EndDateEmployment = dto.EndDateEmployment
            };
        }

        private static void UpdateEntityFromDto(Resource resource, UpdateResourceDto dto)
        {
            if (dto.FirstName != null)
                resource.FirstName = dto.FirstName;
            if (dto.LastName != null)
                resource.LastName = dto.LastName;
            if (dto.Email != null)
                resource.Email = dto.Email;
            if (dto.RoleTitle != null)
                resource.RoleTitle = dto.RoleTitle;
            if (dto.Department != null)
                resource.Department = dto.Department;
            if (dto.Skills != null)
                resource.Skills = dto.Skills;
            if (dto.ExperienceLevel != null)
                resource.ExperienceLevel = dto.ExperienceLevel;
            if (dto.FullTimeEquivalent.HasValue)
                resource.FullTimeEquivalent = dto.FullTimeEquivalent.Value;
            if (dto.WeeklyCapacityHours.HasValue)
                resource.WeeklyCapacityHours = dto.WeeklyCapacityHours;
            if (dto.CostRatePerHour.HasValue)
                resource.CostRatePerHour = dto.CostRatePerHour;
            if (dto.Location != null)
                resource.Location = dto.Location;
            if (dto.EmploymentStatus != null)
                resource.EmploymentStatus = dto.EmploymentStatus;
            if (dto.StartDateEmployment.HasValue)
                resource.StartDateEmployment = dto.StartDateEmployment;
            if (dto.EndDateEmployment.HasValue)
                resource.EndDateEmployment = dto.EndDateEmployment;
        }
    }
} 