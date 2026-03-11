using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrumoMetria.Dto.Auth;
using PrumoMetria.Services.Auth;

namespace PrumoMetria.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAuthService _authService;

    public AccountController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO register)
    {
        var result = await _authService.Register(register);
        
        if (result.Succeeded)
            return Ok("User registered.");

        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO login)
    {
        var response = await _authService.Login(login);

        if (response == null)
            return Unauthorized("Invalid e-mail or password");
        
        return Ok(response);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshDTO request)
    {
        var response = await _authService.Refresh(request);

        if (response == null)
            return Unauthorized("Invalid or expired token.");
        
        return Ok(response);
    }

    [Authorize] 
    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutDTO request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var success = await _authService.Logout(request.AccessToken);

        if (!success)
            return Unauthorized("Invalid token.");
        
        return Ok("Logout successful.");
    }
    
    [Authorize] 
    [HttpGet("me")]
    public async Task<IActionResult> Me()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var user = await _authService.GetMe(userId!);

        if (user == null)
            return NotFound("User not found");
        
        return Ok(user);
    }
}