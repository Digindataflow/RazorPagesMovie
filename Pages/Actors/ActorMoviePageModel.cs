using RazorPagesMovie.Data;
using RazorPagesMovie.Models;
using RazorPagesMovie.Models.MovieViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace RazorPagesMovie.Pages.Actors
{
    public class ActorMoviePageModel : PageModel
    {
        public List<ActedMovieData>? ActedMovieDataList;

        public void PopulateActedMoviesData(RazorPagesMovieContext context,
                                               Actor actor)
        {
            var actorMovies = new HashSet<int> (
                context.ActorMoviePair.Where(i => i.ActorID == actor.ID)
                .Select(c => c.MovieID)
            );

            ActedMovieDataList = new List<ActedMovieData>();
            if (context.Movie != null) {
                // get all movie and if it's directed 
                foreach (var m in context.Movie)
                {
                    ActedMovieDataList.Add(new ActedMovieData
                    {
                        MovieID = m.ID,
                        Title = m.Title,
                        Acted = actorMovies.Contains(m.ID)
                    });
                }
            }

        }
    }
}