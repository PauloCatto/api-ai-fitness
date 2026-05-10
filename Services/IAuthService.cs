using AiFitnessAgent.Api.Models;

namespace AiFitnessAgent.Api.Services;

public interface IAuthService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hash);
    string GenerateToken(User user);
}
