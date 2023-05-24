using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

using RazorPagesMovie.Data;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Directors
{
    [Authorize(Roles = "RequireEditorRole")]
    public class DeleteModel : PageModel
    {
        private readonly RazorPagesMovie.Data.RazorPagesMovieContext _context;

        public DeleteModel(RazorPagesMovie.Data.RazorPagesMovieContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Director Director { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Director == null)
            {
                return NotFound();
            }

            var director = await _context.Director.FirstOrDefaultAsync(m => m.ID == id);

            if (director == null)
            {
                return NotFound();
            }
            else 
            {
                Director = director;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Director == null)
            {
                return NotFound();
            }
            var director = await _context.Director
                .Include(i => i.Movies)
                .SingleAsync(i => i.ID == id);

            if (director == null)
            {
                return RedirectToPage("./Index");
            }
            if (_context.Home != null ) {
                _context.Home.RemoveRange(_context.Home.Where(d => d.DirectorID == id));
                await _context.SaveChangesAsync();
            }

            Director = director;
            _context.Director.Remove(Director);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
