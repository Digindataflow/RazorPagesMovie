using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Studios
{
    public class CreateModel : PageModel
    {
        private readonly RazorPagesMovieContext _context;

        public CreateModel(RazorPagesMovieContext context)
        {
            _context = context;
        }
        [BindProperty]
        public RazorPagesMovie.Models.StudioViewModels.StudioViewModel Studio { get; set; } = null!;
        public SelectList DirectorNameSL { get; set; } = null!;

        public IActionResult OnGet()
        {
            DirectorNameSL = new SelectList(
                _context.Director.Where(i => i.Studio == null), 
                nameof(Director.ID), 
                nameof(Director.FullName)
            );
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Studio == null || Studio == null)
            {
                return Page();
            }

            var newStudio = new Studio();
            _context.Studio.Add(newStudio).CurrentValues.SetValues(Studio);
            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                throw;
            }
            return RedirectToPage("./Index");
        }
    }
}
