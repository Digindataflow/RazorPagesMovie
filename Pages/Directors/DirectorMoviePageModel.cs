using RazorPagesMovie.Data;
using RazorPagesMovie.Models;
using RazorPagesMovie.Models.MovieViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace RazorPagesMovie.Pages.Directors
{
    public class DirectorMoviePageModel : PageModel
    {
        public List<DirectedMoviesData>? DirectedMoviesDataList;

        public void PopulateDirectedMoviesData(RazorPagesMovieContext context,
                                               Director director)
        {
            HashSet<int> directorMovies;

            // get this director's movies 
            if (director.Movies != null ) {
                directorMovies = new HashSet<int>(
                    director.Movies.Select(c => c.ID));
            } else {
                directorMovies = new HashSet<int>();
            }

            DirectedMoviesDataList = new List<DirectedMoviesData>();
            if (context.Movie != null) {
                // get all movie and if it's directed 
                foreach (var m in context.Movie)
                {
                    DirectedMoviesDataList.Add(new DirectedMoviesData
                    {
                        MovieID = m.ID,
                        Title = m.Title,
                        Directed = directorMovies.Contains(m.ID)
                    });
                }
            }

        }
    }
}