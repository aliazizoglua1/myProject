using MyProject.Persistence.Entities;

namespace MyProject.WebAPI.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
        bool ValidateToken(string token);
        Guid? GetUserIdFromToken(string token);
    }
}