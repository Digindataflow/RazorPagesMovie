using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace RazorPagesMovie.Pages.Roles
{
    [Authorize(Roles = "RequireAdministratorRole")]
    public class CreateModel : PageModel
    {
        [BindProperty]
        public IdentityRole Role { get;set; } = null!;
        private readonly RoleManager<IdentityRole> _roleManager;
        public CreateModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || Role == null)
            {
                return Page();
            }

            await _roleManager.CreateAsync(Role);

            return RedirectToPage("./Index");
        }
    }
}
