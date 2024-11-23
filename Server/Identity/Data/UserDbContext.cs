namespace FitApp.Identity.Data;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UserDbContext: IdentityDbContext<IdentityUser>
{
    public UserDbContext(DbContextOptions<UserDbContext> options) :
        base(options)
    { }
}