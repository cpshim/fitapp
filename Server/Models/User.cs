using Microsoft.AspNetCore.Identity;

namespace FitApp.Models;

public class User: IdentityUser
{
    // Identity User class already contains the necessary configuration for Identity tables 
    // public int Id { get; set; }
    // public string? Username { get; set; }
    // public string? Email { get; set; }
    // public string? Password { get; set; }
}