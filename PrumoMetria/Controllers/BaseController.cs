using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PrumoMetria.Controllers;

[Authorize]
[ApiController]
public class BaseController : ControllerBase
{
    protected string GetUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
    }
}