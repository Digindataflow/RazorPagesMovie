using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

using RazorPagesMovie.Data;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Movies
{
    [Authorize(Roles = "RequireEditorRole")]
    public class CreateModel : StudioNamePageModel
    {
        private readonly RazorPagesMovieContext _context;
        [BindProperty]
        public RazorPagesMovie.Models.MovieViewModels.MovieView Movie { get; set; } = default!;

        public CreateModel(RazorPagesMovieContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {   
            PopulateStudiosDropDownList(_context);
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (_context.Movie == null || Movie == null) {
                return NotFound();
            }
            
            if (!ModelState.IsValid)
            {
                PopulateStudiosDropDownList(_context, Movie.StudioID);
                return Page();
            }

            var emptyMovie = new Movie();
            _context.Movie.Add(emptyMovie).CurrentValues.SetValues(Movie);
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
