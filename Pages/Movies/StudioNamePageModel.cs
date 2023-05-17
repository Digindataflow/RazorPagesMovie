using RazorPagesMovie.Data;
using RazorPagesMovie.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace RazorPagesMovie.Pages.Movies
{
    public class StudioNamePageModel : PageModel
    {
        public SelectList? StudioNameSL { get; set; }

        public void PopulateStudiosDropDownList(RazorPagesMovieContext _context,
            object? selectedStudio = null)
        {
            var studiosQuery = from d in _context.Studio
                                   orderby d.Name // Sort by name.
                                   select d;

            // rendered as an HTML <select> element
            // public SelectList (System.Collections.IEnumerable items, 
            // string dataValueField, string dataTextField, object selectedValue);
            StudioNameSL = new SelectList(studiosQuery.AsNoTracking(),
                nameof(Studio.ID),
                nameof(Studio.Name),
                selectedStudio);
        }
    }
}