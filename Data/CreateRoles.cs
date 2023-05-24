using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using RazorPagesMovie.Data;
using RazorPagesMovie.Authorization.Enums;
namespace RazorPagesMovie.Models;

public static class CreateRoles
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();  
        // var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();  

        foreach (var r in Enum.GetNames(typeof(Roles))){
            if (!(await roleManager.RoleExistsAsync(r)))  
            {  
                await roleManager.CreateAsync(new IdentityRole(r));  
            }  
        }
    }
}