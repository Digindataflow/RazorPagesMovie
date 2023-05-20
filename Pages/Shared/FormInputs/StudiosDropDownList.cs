using RazorPagesMovie.Data;
using RazorPagesMovie.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace RazorPagesMovie.Pages.Shared.FormInputs;

public static class StudiosDropDownList {
    public static SelectList Get(RazorPagesMovieContext _context,
        object? selectedStudio = null)
    {
        var studiosQuery = from d in _context.Studio
                                orderby d.Name // Sort by name.
                                select d;

        // rendered as an HTML <select> element
        // public SelectList (System.Collections.IEnumerable items, 
        // string dataValueField, string dataTextField, object selectedValue);
        var StudioNameSL = new SelectList(studiosQuery.AsNoTracking(),
            nameof(Studio.ID),
            nameof(Studio.Name),
            selectedStudio);
        return StudioNameSL;
    }

}
