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

namespace RazorPagesMovie.Pages.Actors
{
    public class EditModel : ActorMoviePageModel
    {
        private readonly RazorPagesMovie.Data.RazorPagesMovieContext _context;

        public EditModel(RazorPagesMovie.Data.RazorPagesMovieContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Actor Actor { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Actor == null)
            {
                return NotFound();
            }

            var actor =  await _context.Actor.FirstOrDefaultAsync(m => m.ID == id);
            if (actor == null)
            {
                return NotFound();
            }
            Actor = actor;
            PopulateActedMoviesData(_context, Actor);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedMovies)
        {
            if (Actor == null || id == null || !ActorExists(Actor.ID))
            {
                return NotFound();
            }
            
            // either use [BindNever] or remove it from ModelState
            // ModelState.Remove("ActorMoviePairs");
            if (!ModelState.IsValid)
            {
                PopulateActedMoviesData(_context, Actor);
                return Page();
            }

            await UpdateActorMoviePairs(selectedMovies, Actor);

            if (await TryUpdateModelAsync<Actor>(
                Actor,
                "Actor",   // Prefix for form value.
                s => s.LastName, s => s.FirstMidName, s => s.BirthDate)) {
                    try {
                        _context.Attach(Actor).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException) {
                        throw;
                    }
                    return RedirectToPage("./Index");
            }
            PopulateActedMoviesData(_context, Actor);
            return Page();
        }

        private bool ActorExists(int id)
        {
          return (_context.Actor?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        public async Task UpdateActorMoviePairs(string[] selectedMovies, Actor actorToUpdate) {

            var selectedMoviesHS = new HashSet<string>(selectedMovies);
            var actorMoviesHS = new HashSet<string> (
                _context.ActorMoviePair.Where(i => i.ActorID == actorToUpdate.ID)
                .Select(c => c.MovieID.ToString())
            );

            var addedMoviesHS = selectedMoviesHS.Except(actorMoviesHS);
            var removedMoviesHS = actorMoviesHS.Except(selectedMoviesHS);

            // remove ActorMoviePairs
            var removedActorMoviePairs = _context.ActorMoviePair
                .Where(i => i.ActorID == actorToUpdate.ID)
                .Where(i => removedMoviesHS.Contains(i.MovieID.ToString()));
            _context.ActorMoviePair.RemoveRange(removedActorMoviePairs);

            // create new ActorMoviePairs
            // var movies = _context.Movie.Where(i => addedMoviesHS.Contains(i.ID.ToString()));
            var newActorMoviePairs = new List<ActorMoviePair> {};
            foreach (var m in addedMoviesHS) {
                newActorMoviePairs.Add(
                    new ActorMoviePair{ActorID = actorToUpdate.ID, MovieID = int.Parse(m)}
                );
            }
            _context.ActorMoviePair.AddRange(newActorMoviePairs);

            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                throw;
            }
        }


    }
}
