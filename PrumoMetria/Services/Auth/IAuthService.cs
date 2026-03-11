using Microsoft.AspNetCore.Identity;
using PrumoMetria.Dto.Auth;

namespace PrumoMetria.Services.Auth;

public interface IAuthService
{
    Task<IdentityResult> Register(RegisterDTO register);
    Task<AuthResponseDTO?> Login(LoginDTO login);
    Task<AuthResponseDTO?> Refresh(RefreshDTO refresh);
    Task<bool> Logout(string accessToken);
    Task<MeDTO?> GetMe(string userId);
}