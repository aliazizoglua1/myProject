using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.Persistence;
using MyProject.Persistence.Entities;
using MyProject.Persistence.Repositories;
using MyProject.WebAPI.DTOs;

namespace MyProject.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationUsersController : ControllerBase
    {
        private readonly IOrganizationUserRepository _organizationUserRepository;
        private readonly ApplicationDbContext _context;

        public OrganizationUsersController(IOrganizationUserRepository organizationUserRepository, ApplicationDbContext context)
        {
            _organizationUserRepository = organizationUserRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrganizationUserDto>>> GetOrganizationUsers()
        {
            var organizationUsers = await _organizationUserRepository.GetAllAsync();
            var organizationUserDtos = organizationUsers.Select(MapToDto);
            return Ok(organizationUserDtos);
        }

        [HttpGet("organization/{organizationId}/user/{userId}")]
        public async Task<ActionResult<OrganizationUserDto>> GetOrganizationUser(Guid organizationId, Guid userId)
        {
            var organizationUser = await _organizationUserRepository.GetByOrganizationAndUserAsync(organizationId, userId);
            if (organizationUser == null)
            {
                return NotFound();
            }
            return Ok(MapToDto(organizationUser));
        }

        [HttpGet("organization/{organizationId}")]
        public async Task<ActionResult<IEnumerable<OrganizationUserDto>>> GetOrganizationUsersByOrganization(Guid organizationId)
        {
            var organizationUsers = await _organizationUserRepository.GetByOrganizationIdAsync(organizationId);
            var organizationUserDtos = organizationUsers.Select(MapToDto);
            return Ok(organizationUserDtos);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<OrganizationUserDto>>> GetOrganizationUsersByUser(Guid userId)
        {
            var organizationUsers = await _organizationUserRepository.GetByUserIdAsync(userId);
            var organizationUserDtos = organizationUsers.Select(MapToDto);
            return Ok(organizationUserDtos);
        }

        [HttpGet("organization/{organizationId}/active")]
        public async Task<ActionResult<IEnumerable<OrganizationUserDto>>> GetActiveOrganizationUsers(Guid organizationId)
        {
            var organizationUsers = await _organizationUserRepository.GetActiveByOrganizationIdAsync(organizationId);
            var organizationUserDtos = organizationUsers.Select(MapToDto);
            return Ok(organizationUserDtos);
        }

        [HttpGet("user/{userId}/active")]
        public async Task<ActionResult<IEnumerable<OrganizationUserDto>>> GetActiveUserOrganizations(Guid userId)
        {
            var organizationUsers = await _organizationUserRepository.GetActiveByUserIdAsync(userId);
            var organizationUserDtos = organizationUsers.Select(MapToDto);
            return Ok(organizationUserDtos);
        }

        [HttpGet("role/{role}")]
        public async Task<ActionResult<IEnumerable<OrganizationUserDto>>> GetOrganizationUsersByRole(string role)
        {
            var organizationUsers = await _organizationUserRepository.GetByRoleAsync(role);
            var organizationUserDtos = organizationUsers.Select(MapToDto);
            return Ok(organizationUserDtos);
        }

        [HttpGet("organization/{organizationId}/role/{role}")]
        public async Task<ActionResult<IEnumerable<OrganizationUserDto>>> GetOrganizationUsersByOrganizationAndRole(Guid organizationId, string role)
        {
            var organizationUsers = await _organizationUserRepository.GetByOrganizationAndRoleAsync(organizationId, role);
            var organizationUserDtos = organizationUsers.Select(MapToDto);
            return Ok(organizationUserDtos);
        }

        [HttpGet("organization/{organizationId}/with-details")]
        public async Task<ActionResult<IEnumerable<OrganizationUserWithDetailsDto>>> GetOrganizationUsersWithDetails(Guid organizationId)
        {
            var organizationUsers = await _context.OrganizationUsers
                .Include(ou => ou.Organization)
                .Include(ou => ou.User)
                .Where(ou => ou.OrganizationId == organizationId)
                .ToListAsync();

            var organizationUserDtos = organizationUsers.Select(MapToDtoWithDetails);
            return Ok(organizationUserDtos);
        }

        [HttpPost]
        public async Task<ActionResult<OrganizationUserDto>> CreateOrganizationUser(CreateOrganizationUserDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the organization-user relationship already exists
            if (await _organizationUserRepository.ExistsAsync(createDto.OrganizationId, createDto.UserId))
            {
                return BadRequest("User is already associated with this organization.");
            }

            var organizationUser = MapToEntity(createDto);
            var createdOrganizationUser = await _organizationUserRepository.AddAsync(organizationUser);
            return CreatedAtAction(nameof(GetOrganizationUser), 
                new { organizationId = createdOrganizationUser.OrganizationId, userId = createdOrganizationUser.UserId }, 
                MapToDto(createdOrganizationUser));
        }

        [HttpPut("organization/{organizationId}/user/{userId}")]
        public async Task<IActionResult> UpdateOrganizationUser(Guid organizationId, Guid userId, UpdateOrganizationUserDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingOrganizationUser = await _organizationUserRepository.GetByOrganizationAndUserAsync(organizationId, userId);
            if (existingOrganizationUser == null)
            {
                return NotFound();
            }

            UpdateEntityFromDto(existingOrganizationUser, updateDto);
            var updatedOrganizationUser = await _organizationUserRepository.UpdateAsync(existingOrganizationUser);
            return Ok(MapToDto(updatedOrganizationUser));
        }

        [HttpDelete("organization/{organizationId}/user/{userId}")]
        public async Task<IActionResult> DeleteOrganizationUser(Guid organizationId, Guid userId)
        {
            var deleted = await _organizationUserRepository.DeleteAsync(organizationId, userId);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("organization/{organizationId}/user/{userId}/is-active")]
        public async Task<ActionResult<bool>> IsUserActiveInOrganization(Guid organizationId, Guid userId)
        {
            var isActive = await _organizationUserRepository.IsUserActiveInOrganizationAsync(organizationId, userId);
            return Ok(isActive);
        }

        private static OrganizationUserDto MapToDto(OrganizationUser organizationUser)
        {
            return new OrganizationUserDto
            {
                OrganizationId = organizationUser.OrganizationId,
                UserId = organizationUser.UserId,
                Role = organizationUser.Role,
                IsActive = organizationUser.IsActive,
                CreatedAt = organizationUser.CreatedAt,
                UpdatedAt = organizationUser.UpdatedAt
            };
        }

        private static OrganizationUserWithDetailsDto MapToDtoWithDetails(OrganizationUser organizationUser)
        {
            return new OrganizationUserWithDetailsDto
            {
                OrganizationId = organizationUser.OrganizationId,
                OrganizationName = organizationUser.Organization?.OrganizationName ?? string.Empty,
                UserId = organizationUser.UserId,
                UserEmail = organizationUser.User?.Email ?? string.Empty,
                UserFirstName = organizationUser.User?.FirstName ?? string.Empty,
                UserLastName = organizationUser.User?.LastName ?? string.Empty,
                Role = organizationUser.Role,
                IsActive = organizationUser.IsActive,
                CreatedAt = organizationUser.CreatedAt,
                UpdatedAt = organizationUser.UpdatedAt
            };
        }

        private static OrganizationUser MapToEntity(CreateOrganizationUserDto dto)
        {
            return new OrganizationUser
            {
                OrganizationId = dto.OrganizationId,
                UserId = dto.UserId,
                Role = dto.Role,
                IsActive = dto.IsActive
            };
        }

        private static void UpdateEntityFromDto(OrganizationUser organizationUser, UpdateOrganizationUserDto dto)
        {
            if (!string.IsNullOrEmpty(dto.Role))
                organizationUser.Role = dto.Role;

            if (dto.IsActive.HasValue)
                organizationUser.IsActive = dto.IsActive.Value;
        }
    }
} 