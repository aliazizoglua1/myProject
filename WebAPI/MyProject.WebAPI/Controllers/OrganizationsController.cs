using Microsoft.AspNetCore.Mvc;
using MyProject.Persistence.Entities;
using MyProject.Persistence.Repositories;
using MyProject.WebAPI.DTOs;

namespace MyProject.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationRepository _organizationRepository;

        public OrganizationsController(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrganizationDto>>> GetOrganizations()
        {
            var organizations = await _organizationRepository.GetAllAsync();
            var organizationDtos = organizations.Select(MapToDto);
            return Ok(organizationDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrganizationDto>> GetOrganization(Guid id)
        {
            var organization = await _organizationRepository.GetByIdAsync(id);
            if (organization == null)
            {
                return NotFound();
            }
            return Ok(MapToDto(organization));
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<OrganizationDto>> GetOrganizationByName(string name)
        {
            var organization = await _organizationRepository.GetByNameAsync(name);
            if (organization == null)
            {
                return NotFound();
            }
            return Ok(MapToDto(organization));
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<OrganizationDto>>> GetOrganizationsByStatus(string status)
        {
            var organizations = await _organizationRepository.GetByStatusAsync(status);
            var organizationDtos = organizations.Select(MapToDto);
            return Ok(organizationDtos);
        }

        [HttpPost]
        public async Task<ActionResult<OrganizationDto>> CreateOrganization(CreateOrganizationDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if organization name already exists
            if (await _organizationRepository.ExistsByNameAsync(createDto.OrganizationName))
            {
                return BadRequest("Organization with this name already exists.");
            }

            var organization = MapToEntity(createDto);
            var createdOrganization = await _organizationRepository.AddAsync(organization);
            return CreatedAtAction(nameof(GetOrganization), new { id = createdOrganization.OrganizationId }, MapToDto(createdOrganization));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrganization(Guid id, UpdateOrganizationDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingOrganization = await _organizationRepository.GetByIdAsync(id);
            if (existingOrganization == null)
            {
                return NotFound();
            }

            // Check if the new name conflicts with another organization
            if (!string.IsNullOrEmpty(updateDto.OrganizationName) && 
                updateDto.OrganizationName != existingOrganization.OrganizationName)
            {
                if (await _organizationRepository.ExistsByNameAsync(updateDto.OrganizationName))
                {
                    return BadRequest("Organization with this name already exists.");
                }
            }

            UpdateEntityFromDto(existingOrganization, updateDto);
            var updatedOrganization = await _organizationRepository.UpdateAsync(existingOrganization);
            return Ok(MapToDto(updatedOrganization));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganization(Guid id)
        {
            var deleted = await _organizationRepository.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        private static OrganizationDto MapToDto(Organization organization)
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

        private static Organization MapToEntity(CreateOrganizationDto dto)
        {
            return new Organization
            {
                OrganizationName = dto.OrganizationName,
                Status = dto.Status,
                BillingPlan = dto.BillingPlan,
                ContactEmail = dto.ContactEmail
            };
        }

        private static void UpdateEntityFromDto(Organization organization, UpdateOrganizationDto dto)
        {
            if (!string.IsNullOrEmpty(dto.OrganizationName))
                organization.OrganizationName = dto.OrganizationName;

            if (!string.IsNullOrEmpty(dto.Status))
                organization.Status = dto.Status;

            if (dto.BillingPlan != null)
                organization.BillingPlan = dto.BillingPlan;

            if (dto.ContactEmail != null)
                organization.ContactEmail = dto.ContactEmail;
        }
    }
} 