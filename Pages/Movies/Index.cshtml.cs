using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using RazorPagesMovie.Utils;
using RazorPagesMovie.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RazorPagesMovie.Pages.Movies
{
    [Authorize(Roles = "RequireReaderRole")]
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovie.Data.RazorPagesMovieContext _context;
        private readonly IConfiguration Configuration;

        public IndexModel(RazorPagesMovie.Data.RazorPagesMovieContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public PaginatedList<Movie> Movie { get;set; } = default!;

        // sort, paging, search 
        public string? TitleSort { get; set; }
        public string? DateSort { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? CurrentSort { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? CurrentFilter { get; set; }

        public SelectList? Genres { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? MovieGenre { get; set; }

        public async Task OnGetAsync(int? pageIndex)
        {
            // get order by 
            TitleSort = string.IsNullOrEmpty(CurrentSort) ? "title_desc" : "";
            DateSort = CurrentSort == "Date" ? "date_desc" : "Date";
            // get Genre choices 
            IQueryable<string> genreQuery = from m in _context.Movie
                                orderby m.Genre
                                select m.Genre;
            Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            // creates a LINQ query to select the movies
            var movies = from m in _context.Movie
                        select m;
            if (SearchString != null)
            {
                pageIndex = 1;
                CurrentFilter = SearchString;
            }

            // filter by SearchString and MovieGenre
            if (!string.IsNullOrEmpty(CurrentFilter))
            {
                movies = movies.Where(s => s.Title.Contains(CurrentFilter));
            }
            if (!string.IsNullOrEmpty(MovieGenre))
            {
                movies = movies.Where(x => x.Genre == MovieGenre);
            }

            // order results 
            switch (CurrentSort)
            {
                case "title_desc":
                    movies = movies.OrderByDescending(s => s.Title);
                    break;
                case "Date":
                    movies = movies.OrderBy(s => s.ReleaseDate);
                    break;
                case "date_desc":
                    movies = movies.OrderByDescending(s => s.ReleaseDate);
                    break;
                default:
                    movies = movies.OrderBy(s => s.Title);
                    break;
            }


            // get record list 
            // data is not cached and tracked, save RAM 
            var pageSize = Configuration.GetValue("PageSize", 20);
            Movie = await PaginatedList<Movie>.CreateAsync(
                movies.AsNoTracking().Include(s => s.Studio), 
                pageIndex ?? 1, 
                pageSize
            );
        }


    }
}
