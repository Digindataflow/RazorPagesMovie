using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

using RazorPagesMovie.Data;
using RazorPagesMovie.Models;
using RazorPagesMovie.Pages.Shared.FormInputs;

namespace RazorPagesMovie.Pages.Directors
{
    [Authorize(Roles = "RequireEditorRole")]
    public class EditModel : DirectorMoviePageModel
    {
        private readonly RazorPagesMovie.Data.RazorPagesMovieContext _context;
        private readonly ILogger<EditModel> _logger;

        public EditModel(RazorPagesMovie.Data.RazorPagesMovieContext context, ILogger<EditModel> logger)
        {
            _context = context;
            _logger = logger; 
        }

        [BindProperty]
        public Director Director { get; set; } = default!;
        public SelectList? StudioNameSL { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Director == null)
            {
                return NotFound();
            }

            var director =  await _context.Director
                .Include(i => i.Movies)
                .Include(i => i.Home) 
                .Include(i => i.Studio)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (director == null)
            {
                return NotFound();
            }

            // set up form input data
            Director = director;
            PopulateDirectedMoviesData(_context, Director);
            StudioNameSL = StudiosDropDownList.Get(_context, director.Studio?.ID);
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
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Director == null || id == null || !DirectorExists(Director.ID) || directorToUpdate == null)
            {
                return NotFound();
            }

            ModelState.Remove("Director.Home.Director");
            ModelState.Remove("Director.Studio.Name");
            if (!ModelState.IsValid)
            {
                PopulateDirectedMoviesData(_context, directorToUpdate);
                StudioNameSL = StudiosDropDownList.Get(_context, directorToUpdate.Studio?.ID);
                return Page();
            }

            _context.Entry(directorToUpdate).CurrentValues.SetValues(Director);
            UpdateDirectorHome(directorToUpdate);
            UpdateDirectorStudio(directorToUpdate);
            UpdateDirectorMovies(selectedMovies, directorToUpdate);

            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                throw;
            }
            PopulateDirectedMoviesData(_context, directorToUpdate);
            StudioNameSL = StudiosDropDownList.Get(_context, directorToUpdate.Studio?.ID);
            return RedirectToPage("./Index");
        }

        private bool DirectorExists(int id)
        {
          return (_context.Director?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        public void UpdateDirectorHome(Director directorToUpdate) {
            // if home is empty, set it to null 
            if (String.IsNullOrWhiteSpace(Director.Home?.Location)) {
                directorToUpdate.Home = null;
                return;
            }
            if (directorToUpdate.Home == null) {
                directorToUpdate.Home = new Home{};
            }
            directorToUpdate.Home.Location = Director.Home.Location;
            directorToUpdate.Home.Director = directorToUpdate;
        }


        public void UpdateDirectorStudio(Director directorToUpdate) {
            // if Studio is empty, set it to null 
            if (Director.Studio is null) {
                directorToUpdate.Studio = null;
                return;
            }
            var newSdutio = _context.Studio.Where(i => i.ID == Director.Studio.ID).Single();
            directorToUpdate.Studio = newSdutio;
        }

        public void UpdateDirectorMovies(string[] selectedMovies, Director directorToUpdate) {
            // if nothing selected, initialize as empty
            if (selectedMovies == null)
            {
                directorToUpdate.Movies = new List<Movie>();
                return;
            }
            // get selected movies and set navigation property 
            var selectedMovieIDs = selectedMovies.Select(int.Parse).ToArray();
            var selectedMovieInstances = _context.Movie
                .Where(i => selectedMovieIDs.Any(j => j == i.ID))
                .ToList();
            directorToUpdate.Movies = selectedMovieInstances;

        }
    }
}
