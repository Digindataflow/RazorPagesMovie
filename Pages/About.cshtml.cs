using RazorPagesMovie.Models.MovieViewModels;
using RazorPagesMovie.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RazorPagesMovie.Models;


namespace RazorPagesMovie.Pages
{
    [AllowAnonymous]
    public class AboutModel : PageModel
    {
        private readonly RazorPagesMovieContext _context;

        public AboutModel(RazorPagesMovieContext context)
        {
            _context = context;
        }

        public IList<ActorMovieDateGroup> Actors { get; set; } = default!;

        public async Task OnGetAsync()
        {
            IQueryable<ActorMovieDateGroup> data =
                from actor in _context.Actor
                group actor by actor.BirthDate into dateGroup
                select new ActorMovieDateGroup()
                {
                    BirthDate = dateGroup.Key,
                    ActorCount = dateGroup.Count()
                };

            Actors = await data.AsNoTracking().ToListAsync();
        }
    }
}