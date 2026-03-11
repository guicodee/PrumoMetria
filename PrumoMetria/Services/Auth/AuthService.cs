using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PrumoMetria.Dto.Auth;
using PrumoMetria.Entities;
using PrumoMetria.Helpers;
using PrumoMetria.Settings;

namespace PrumoMetria.Services.Auth;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtHelper _jwtHelper;
    private readonly JwtSettings _jwtSettings;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        JwtHelper jwtHelper,
        IOptions<JwtSettings> jwtSettings)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtHelper = jwtHelper;
        _jwtSettings = jwtSettings.Value;
    }
    
    public async Task<IdentityResult> Register(RegisterDTO register)
    {
        var user = new ApplicationUser
        {
            UserName = register.Email,
            Email = register.Email,
            Name = register.Name
        };

        var result = await _userManager.CreateAsync(user, register.Password);

        return result;
    }

    public async Task<AuthResponseDTO?> Login(LoginDTO login)
    {
        var user = await _userManager.FindByEmailAsync(login.Email);
        if (user == null)
            return null;

        var passwordChecked = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
        if (!passwordChecked.Succeeded)
            return null;
        
        var result = await GenerateAndSaveTokensAsync(user);

        return result;
    }

    public async Task<AuthResponseDTO?> Refresh(RefreshDTO refresh)
    {
        var user = await GetUserFromTokenAsync(refresh.AccessToken);
        if (user == null)
            return null;

        var savedRefreshToken = await _userManager.GetAuthenticationTokenAsync(
            user,
            _jwtSettings.LoginProvider,
            _jwtSettings.RefreshTokenName
        );

        if (savedRefreshToken != refresh.RefreshToken)
            return null;

        var result = await GenerateAndSaveTokensAsync(user);

        return result;
    }

    public async Task<bool> Logout(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return false;

        await _userManager.RemoveAuthenticationTokenAsync(
            user,
            _jwtSettings.LoginProvider,
            _jwtSettings.RefreshTokenName
        );

        return true;
    }

    public async Task<MeDTO?> GetMe(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId!);
        if (user == null)
            return null;

        var result = new MeDTO(user.Id, user.Email!, user.Name ?? "");

        return result;
    }

    private async Task<AuthResponseDTO> GenerateAndSaveTokensAsync(ApplicationUser user)
    {
        var accessToken = _jwtHelper.GenerateAccessToken(user);
        var refreshToken = _jwtHelper.GenerateRefreshToken();

        await _userManager.SetAuthenticationTokenAsync(
            user, 
            _jwtSettings.LoginProvider,
            _jwtSettings.RefreshTokenName, 
            refreshToken
        );

        return new AuthResponseDTO(accessToken, refreshToken, 3600);
    }
    
    private async Task<ApplicationUser?> GetUserFromTokenAsync(string accessToken)
    {
        var principal = _jwtHelper.GetPrincipalFromExpiredToken(accessToken);
        if (principal == null) return null;

        var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        return await _userManager.FindByIdAsync(userId!);
    }
}