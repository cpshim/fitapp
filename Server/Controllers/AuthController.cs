using FitApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FitApp.Controllers;

[ApiController]
[Route("/api/[controller]")]
// [Authorize]
public class AuthController: ControllerBase
{
    [HttpPost("logout")]
    public async Task<IActionResult> Logout(SignInManager<User> signInManager, [FromBody] object empty)
    {
        if (empty != null)
        {
            await signInManager.SignOutAsync();
            // HttpContext.Session.Clear();
            return Ok(new { message = "Successfully logged out" });
        }
        return Unauthorized();
    }
    
     [HttpPost("login")]
     public async Task<IActionResult> Login(SignInManager<User> signInManager, UserManager<User> userManager, [FromBody] LoginModel model)
     {
         if (ModelState.IsValid)
         {
             var user = await userManager.FindByEmailAsync(model.Email);
             if (user == null)
             {
                 return Unauthorized("Invalid login attempt.");
             }

             var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
             if (result.Succeeded)
             {
                 // Optionally, store some session data if needed
                 HttpContext.Session.SetString("UserName", model.Email);

                 // You may need to issue a cookie manually here
                 // await signInManager.SignInAsync(user, isPersistent: false);

                 return Ok(new { message = "Login successful" });
             }
         }

         return Unauthorized("Invalid login attempt.");
     }
}