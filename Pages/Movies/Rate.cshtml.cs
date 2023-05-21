using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Movies
{
    public class RateModel : PageModel
    {
        private readonly RazorPagesMovieContext _context;

        public RateModel(RazorPagesMovieContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RazorPagesMovie.Models.MovieViewModels.MovieRatingView MovieRating { get; set; } = null!;
        public Movie Movie { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.MovieRating == null)
            {
                return NotFound();
            }
            var MovieID = id.Value;
            var movie = await _context.Movie
                .Include(c => c.Star)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == MovieID);
            if (movie == null) {
                return NotFound();
            }
            Movie = movie;

            var movieRating = movie.Star;

            if (movieRating is null) {
                MovieRating = new RazorPagesMovie.Models.MovieViewModels.MovieRatingView {
                    MovieID = MovieID,
                };
            }
            else {
                MovieRating = new RazorPagesMovie.Models.MovieViewModels.MovieRatingView {
                    MovieID = MovieID,
                    Star = movieRating.Star,
                    Comment = movieRating.Comment
                };
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (MovieRating == null || id == null || _context.MovieRating == null)
            {
                return NotFound();
            }
            var MovieID = id.Value;
            var newMovieRating = await _context.MovieRating
                .Include(c => c.Movie)
                .FirstOrDefaultAsync(m => m.MovieID == MovieID);

            // create new MovieRating
            if (newMovieRating is null) {
                newMovieRating = new MovieRating ();
                _context.Add(newMovieRating).CurrentValues.SetValues(MovieRating);
            }
            else {
                _context.Entry(newMovieRating).CurrentValues.SetValues(MovieRating);
            }
            
            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!MovieExists(MovieID)) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }
            return RedirectToPage("./Index");

        }

        private bool MovieExists(int id)
        {
          return (_context.Movie?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
