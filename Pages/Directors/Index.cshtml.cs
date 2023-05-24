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
using RazorPagesMovie.Models.MovieViewModels;

namespace RazorPagesMovie.Pages.Directors
{
    [Authorize(Roles = "RequireReaderRole")]
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovieContext _context;

        public IndexModel(RazorPagesMovieContext context)
        {
            _context = context;
        }

        public DirectorIndexData? DirectorIndexData { get; set; }
        public int DirectorID { get; set; }
        public int MovieID { get; set; }

        public async Task OnGetAsync(int? id, int? movieID)
        {
            DirectorIndexData = new DirectorIndexData();
            if (_context.Director != null)
            {
                DirectorIndexData.Directors = await _context.Director
                    .Include(i => i.Home!)                 
                    .Include(i => i.Movies!)
                        .ThenInclude(c => c.Studio)
                    .OrderBy(i => i.LastName)
                    .ToListAsync();
            }
            if (id != null && DirectorIndexData.Directors != null)
            {
                DirectorID = id.Value;
                Director director = DirectorIndexData.Directors
                    .Where(i => i.ID == id.Value).Single();
                DirectorIndexData.Movies = director.Movies;
            }
            if (movieID != null && _context.ActorMoviePair != null)
            {
                MovieID = movieID.Value;
                IEnumerable<ActorMoviePair> ActorMoviePairs = await _context.ActorMoviePair
                    .Where(x => x.MovieID == MovieID)                    
                    .Include(i=>i.Actor)
                    .ToListAsync();                 
                DirectorIndexData.ActorMoviePairs = ActorMoviePairs;
            }
        }
    }
}
