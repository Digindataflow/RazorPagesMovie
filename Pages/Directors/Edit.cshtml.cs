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

namespace RazorPagesMovie.Pages.Directors
{
    public class EditModel : DirectorMoviePageModel
    {
        private readonly RazorPagesMovie.Data.RazorPagesMovieContext _context;

        public EditModel(RazorPagesMovie.Data.RazorPagesMovieContext context)
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

            var director =  await _context.Director
                .Include(i => i.Movies)
                .Include(i => i.Home) 
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (director == null)
            {
                return NotFound();
            }
            Director = director;
            PopulateDirectedMoviesData(_context, Director);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedMovies)
        {
            var directorToUpdate = await _context.Director
                .Include(i => i.Movies)
                .Include(i => i.Home) 
                .Include(i => i.Studio)
                // .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Director == null || id == null || !DirectorExists(Director.ID) || directorToUpdate == null)
            {
                return NotFound();
            }

            ModelState.Remove("Director.Home.Director");
            if (!ModelState.IsValid)
            {
                PopulateDirectedMoviesData(_context, directorToUpdate);
                return Page();
            }

            UpdateDirectorHome(directorToUpdate);
            UpdateDirectorProperties(directorToUpdate);

            // if (await TryUpdateModelAsync<Director>(
            //     directorToUpdate,
            //     "Director",   // Prefix for form value.
            //     s => s.LastName, s => s.FirstMidName, s => s.HireDate, s => s.Home)) {

            // _context.Attach(directorToUpdate).State = EntityState.Modified;
            // _context.Entry(Director).Collection(i => i.Movies).Load();
            // _context.Entry(Director).Reference(i => i.Studio).Load();

            UpdateDirectorMovies(selectedMovies, directorToUpdate);

            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                throw;
            }
            return RedirectToPage("./Index");
            // }
            // UpdateDirectorMovies(selectedMovies, directorToUpdate);
            // PopulateDirectedMoviesData(_context, directorToUpdate);
            // return Page();
        }

        private bool DirectorExists(int id)
        {
          return (_context.Director?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        public void UpdateDirectorProperties(Director directorToUpdate) {
            directorToUpdate.LastName = Director.LastName;
            directorToUpdate.FirstMidName = Director.FirstMidName;
            directorToUpdate.HireDate = Director.HireDate;
        }

        public void UpdateDirectorHome(Director directorToUpdate) {
            // if home is empty, set it to null 
            if (String.IsNullOrWhiteSpace(Director.Home?.Location)) {
                directorToUpdate.Home = null;
                return;
            }
            // var home = _context.Home
            //     // .AsNoTracking()
            //     .SingleOrDefault(i => i.DirectorID == directorToUpdate.ID);
            // if (home != null) {
            //     directorToUpdate.Home.ID = home.ID;
            // }
            if (directorToUpdate.Home == null) {
                directorToUpdate.Home = new Home{};
            }
            directorToUpdate.Home.Location = Director.Home.Location;
            directorToUpdate.Home.Director = directorToUpdate;
        }

        public void UpdateDirectorMovies(string[] selectedMovies, Director directorToUpdate) {
            // if nothing selected, initialize as empty
            if (selectedMovies == null)
            {
                directorToUpdate.Movies = new List<Movie>();
                return;
            }

            var selectedMoviesHS = new HashSet<string>(selectedMovies);
            HashSet<int> directorMovies;

            if (directorToUpdate.Movies != null) {
                directorMovies = new HashSet<int>
                (directorToUpdate.Movies.Select(c => c.ID));
            }
            else {
                directorMovies = new HashSet<int>();
                directorToUpdate.Movies = new List<Movie>();
            }

            foreach (var movie in _context.Movie)
            {
                if (selectedMoviesHS.Contains(movie.ID.ToString()))
                {
                    if (!directorMovies.Contains(movie.ID))
                    {
                        directorToUpdate.Movies.Add(movie);
                    }
                }
                else
                {
                    if (directorMovies.Contains(movie.ID))
                    {
                        var movieToRemove = directorToUpdate.Movies.Single(
                                                        c => c.ID == movie.ID);
                        directorToUpdate.Movies.Remove(movieToRemove);
                    }
                }
            }

        }
    }
}
