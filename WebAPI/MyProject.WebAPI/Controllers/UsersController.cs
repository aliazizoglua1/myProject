using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MyProject.Persistence.Entities;
using MyProject.Persistence.Repositories;
using MyProject.WebAPI.DTOs;
using MyProject.WebAPI.Services;
using BCrypt.Net;
using System.Security.Cryptography;
using System.Text;

namespace MyProject.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _jwtTokenService;

        public UsersController(IUserRepository userRepository, IJwtTokenService jwtTokenService)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _userRepository.GetAllAsync();
            var userDtos = users.Select(MapToDto);
            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetUser(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(MapToDto(user));
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<UserDto>> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(MapToDto(user));
        }

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetActiveUsers()
        {
            var users = await _userRepository.GetActiveUsersAsync();
            var userDtos = users.Select(MapToDto);
            return Ok(userDtos);
        }

        [HttpGet("inactive")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetInactiveUsers()
        {
            var users = await _userRepository.GetInactiveUsersAsync();
            var userDtos = users.Select(MapToDto);
            return Ok(userDtos);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if email already exists
            if (await _userRepository.ExistsByEmailAsync(createDto.Email))
            {
                return BadRequest("User with this email already exists.");
            }

            var user = MapToEntity(createDto);
            var createdUser = await _userRepository.AddAsync(user);
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.UserId }, MapToDto(createdUser));
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login(UserLoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userRepository.GetByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            if (!user.IsActive)
            {
                return Unauthorized("Account is deactivated.");
            }

            if (!VerifyPassword(loginDto.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid email or password.");
            }

            // If password was verified with old SHA256 method, update to BCrypt
            if (IsLegacyPasswordHash(user.PasswordHash))
            {
                user.PasswordHash = HashPassword(loginDto.Password);
                await _userRepository.UpdateAsync(user);
            }

            var token = _jwtTokenService.GenerateToken(user);
            var expiresAt = DateTime.UtcNow.AddMinutes(60); // Should match JWT configuration

            return Ok(new LoginResponseDto
            {
                Token = token,
                User = MapToDto(user),
                ExpiresAt = expiresAt
            });
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(Guid id, UpdateUserDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            // Check if the new email conflicts with another user
            if (!string.IsNullOrEmpty(updateDto.Email) && 
                updateDto.Email != existingUser.Email)
            {
                if (!await _userRepository.IsEmailUniqueAsync(updateDto.Email, id))
                {
                    return BadRequest("User with this email already exists.");
                }
            }

            UpdateEntityFromDto(existingUser, updateDto);
            var updatedUser = await _userRepository.UpdateAsync(existingUser);
            return Ok(MapToDto(updatedUser));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var deleted = await _userRepository.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        private static UserDto MapToDto(User user)
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

        private static User MapToEntity(CreateUserDto dto)
        {
            return new User
            {
                Email = dto.Email,
                PasswordHash = HashPassword(dto.Password),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                IsActive = dto.IsActive
            };
        }

        private static void UpdateEntityFromDto(User user, UpdateUserDto dto)
        {
            if (!string.IsNullOrEmpty(dto.Email))
                user.Email = dto.Email;

            if (!string.IsNullOrEmpty(dto.Password))
                user.PasswordHash = HashPassword(dto.Password);

            if (!string.IsNullOrEmpty(dto.FirstName))
                user.FirstName = dto.FirstName;

            if (!string.IsNullOrEmpty(dto.LastName))
                user.LastName = dto.LastName;

            if (dto.IsActive.HasValue)
                user.IsActive = dto.IsActive.Value;
        }

        private static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private static bool VerifyPassword(string password, string passwordHash)
        {
            // Try BCrypt first (new method)
            try
            {
                return BCrypt.Net.BCrypt.Verify(password, passwordHash);
            }
            catch (BCrypt.Net.SaltParseException)
            {
                // If BCrypt fails, try legacy SHA256 method
                return VerifyLegacyPassword(password, passwordHash);
            }
        }

        private static bool VerifyLegacyPassword(string password, string passwordHash)
        {
            try
            {
                using var sha256 = SHA256.Create();
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var legacyHash = Convert.ToBase64String(hashedBytes);
                return legacyHash == passwordHash;
            }
            catch
            {
                return false;
            }
        }

        private static bool IsLegacyPasswordHash(string passwordHash)
        {
            // BCrypt hashes always start with $2a$, $2b$, or $2y$
            // Legacy SHA256 hashes are base64 strings that don't start with $
            return !passwordHash.StartsWith("$2");
        }
    }
} 