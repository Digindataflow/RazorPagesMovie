using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Directors
{
    public class CreateModel : DirectorMoviePageModel
    {
        private readonly RazorPagesMovie.Data.RazorPagesMovieContext _context;
        private readonly ILogger<DirectorMoviePageModel> _logger;

        public CreateModel(RazorPagesMovie.Data.RazorPagesMovieContext context, ILogger<DirectorMoviePageModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            var director = new Director();
            director.Movies = new List<Movie>();

            // Provides an empty collection for the foreach loop
            // foreach (var movie in Model.DirectedMoviesDataList)
            // in the Create Razor page.
            PopulateDirectedMoviesData(_context, director);

            return Page();
        }

        [BindProperty]
        public Director Director { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string[] selectedMovies)
        {
          if (!ModelState.IsValid || _context.Director == null || Director == null)
            {
                return Page();
            }

            var newDirector = new Director();
            newDirector.Movies = new List<Movie>();

            if (selectedMovies.Length > 0 && _context.Movie != null) {
                // Load collection with one DB call.
                _context.Movie.ToList();
            }
            if (_context.Movie != null) {
                // Add selected movies to the new director.
                foreach (var m in selectedMovies)
                {
                    var foundMovie = await _context.Movie.FindAsync(int.Parse(m));
                    if (foundMovie != null){
                        newDirector.Movies.Add(foundMovie);
                    }
                    else{
                        _logger.LogWarning("Movie {m} not found", m);
                    }
                }
            }

            try
            {
                if (await TryUpdateModelAsync<Director>(
                                newDirector,
                                "Director",
                                i => i.FirstMidName, i => i.LastName,
                                i => i.HireDate, i => i.Home))
                {
                    _context.Director.Add(newDirector);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            PopulateDirectedMoviesData(_context, newDirector);
            return Page();
        }
    }
}
