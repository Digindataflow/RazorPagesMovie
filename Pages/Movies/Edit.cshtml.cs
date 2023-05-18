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

namespace RazorPagesMovie.Pages.Movies
{
    public class EditModel : StudioNamePageModel
    {
        private readonly RazorPagesMovie.Data.RazorPagesMovieContext _context;

        public EditModel(RazorPagesMovie.Data.RazorPagesMovieContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Movie Movie { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie =  await _context.Movie
                .Include(c => c.Studio)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }
            Movie = movie;
            PopulateStudiosDropDownList(_context, Movie.StudioID);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Movie == null)
            {
                return NotFound();
            }

            _context.Attach(Movie).State = EntityState.Modified;

            // limit which fields to be got from form value in PageContext 
            // avoid overposting 
            if (await TryUpdateModelAsync<Movie>(
                Movie,
                "Movie",   // Prefix for form value.
                s => s.Title, s => s.ReleaseDate, s => s.Price, s => s.Genre, s => s.Rating, s => s.StudioID)) {
                try {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!MovieExists(Movie.ID)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToPage("./Index");
            }
        
            // Select StudioID if TryUpdateModelAsync fails.
            PopulateStudiosDropDownList(_context, Movie.StudioID);
            return Page();
        }

        private bool MovieExists(int id)
        {
          return (_context.Movie?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
