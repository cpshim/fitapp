using FitApp.Models;
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
    [HttpPost]
    public async Task<IActionResult> Logout(SignInManager<User> signInManager, [FromBody] object empty)
    {
        if (empty != null)
        {
            await signInManager.SignOutAsync();
            return Ok(new { message = "Successfully logged out" });
        }
        return Unauthorized();
    }
}