using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using RazorPagesMovie.Data;

namespace RazorPagesMovie.Models;

public static class CreateRoles
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        // using (var context = new RazorPagesMovieContext(
        //     serviceProvider.GetRequiredService<DbContextOptions<RazorPagesMovieContext>>()))
        // {
        //     if (context == null)
        //     {
        //         throw new ArgumentNullException("Null RazorPagesMovieContext");
        //     }
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();  
            // var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();  

            var roles = new string[] {"Administrator", "Editor", "Reader"};
            foreach (var r in roles) {
                if (!(await roleManager.RoleExistsAsync(r)))  
                {  
                    await roleManager.CreateAsync(new IdentityRole(r));  
                }  
            }

        // }
    }
}