using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages_Movies
{
    public class CreateModel : PageModel
    {
        private readonly RazorPagesMovie.Data.RazorPagesMovieContext _context;

        public CreateModel(RazorPagesMovie.Data.RazorPagesMovieContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Movie Movie { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Movie == null || Movie == null)
            {
                return Page();
            }

            var emptyMovie = new Movie();
            // limit which fields to be got from form value in PageContext 
            // avoid overposting 
            if (await TryUpdateModelAsync<Movie>(
                emptyMovie,
                "movie",   // Prefix for form value.
                s => s.Title, s => s.ReleaseDate, s => s.Price, s => s.Genre, s => s.Rating)) 
            {
                _context.Movie.Add(Movie);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            return Page();

        }
    }
}