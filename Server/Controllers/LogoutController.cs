using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FitApp.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class LogoutController: ControllerBase
{
    [HttpPost("logout")]
    public async Task<IActionResult> Logout(SignInManager<IdentityUser> signInManager, [FromBody] object empty)
    {
        await signInManager.SignOutAsync();
        return Ok(new { message = "Successfully logged out" });
    }
}