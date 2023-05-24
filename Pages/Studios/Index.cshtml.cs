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

namespace RazorPagesMovie.Pages.Studios
{
    [Authorize(Roles = "RequireReaderRole")]
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovieContext _context;

        public IndexModel(RazorPagesMovieContext context)
        {
            _context = context;
        }

        public IList<Studio> Studios { get;set; } = null!;

        public async Task OnGetAsync()
        {
            if (_context.Studio != null)
            {
                Studios = await _context.Studio
                    .Include(s => s.Director)
                    .ToListAsync();
            }
        }
    }
}
