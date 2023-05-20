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

namespace RazorPagesMovie.Pages.Studios
{
    public class EditModel : PageModel
    {
        private readonly RazorPagesMovie.Data.RazorPagesMovieContext _context;

        public EditModel(RazorPagesMovie.Data.RazorPagesMovieContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Studio? Studio { get; set; }
        public SelectList? DirectorNameSL { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Studio == null)
            {
                return NotFound();
            }

            var studio =  await _context.Studio
                .Include(d => d.Director) 
                .AsNoTracking()   
                .FirstOrDefaultAsync(m => m.ID == id);
            if (studio == null)
            {
                return NotFound();
            }

            Studio = studio;
            // Use strongly typed data rather than ViewData.
            DirectorNameSL = new SelectList(_context.Director, "ID", "FirstMidName");
            // ViewData["DirectorID"] = new SelectList(_context.Director, "ID", "FirstMidName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Studio == null || _context.Studio == null ) {
                return NotFound();
            }
            // Fetch current department from DB.
            // ConcurrencyToken may have changed.
            var studioToUpdate = await _context.Studio
                .Include(i => i.Director)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (studioToUpdate == null)
            {
                return HandleDeletedStudio();
            }

            if (studioToUpdate.ID != Studio.ID) {
                return NotFound();
            }

            // _context.Attach(Studio).State = EntityState.Modified;
            studioToUpdate.ConcurrencyToken = Guid.NewGuid();

            // Set ConcurrencyToken to value read in OnGetAsync
            _context.Entry(studioToUpdate).Property(d => d.ConcurrencyToken)
                                   .OriginalValue = Studio.ConcurrencyToken;

            if (await TryUpdateModelAsync<Studio>(
                studioToUpdate,
                "Studio",
                s => s.Name, s => s.StartDate, s => s.Budget, s => s.DirectorID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var exceptionEntry = ex.Entries.Single();
                    var clientValues = (Studio)exceptionEntry.Entity;
                    var databaseEntry = exceptionEntry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty, "Unable to save. " +
                            "The studio was deleted by another user.");
                        return Page();
                    }

                    var dbValues = (Studio)databaseEntry.ToObject();
                    await SetDbErrorMessage(dbValues, clientValues, _context);

                    // Save the current ConcurrencyToken so next postback
                    // matches unless an new concurrency issue happens.
                    Studio.ConcurrencyToken = dbValues.ConcurrencyToken;
                    // Clear the model error for the next postback.
                    ModelState.Remove($"{nameof(Studio)}.{nameof(Studio.ConcurrencyToken)}");
                }
            }

            DirectorNameSL = new SelectList(_context.Director, "ID", "FullName", studioToUpdate.DirectorID);
            return Page();
        }

        private IActionResult HandleDeletedStudio(){
            // ModelState contains the posted data because of the deletion error
            // and overides the Studio instance values when displaying Page().
            ModelState.AddModelError(string.Empty,
                "Unable to save. The studio was deleted by another user.");
            DirectorNameSL = new SelectList(_context.Director, "ID", "FullName", Studio?.DirectorID);
            return Page();
        }

        private async Task SetDbErrorMessage(Studio dbValues,
                Studio clientValues, RazorPagesMovieContext context)
        {

            if (dbValues.Name != clientValues.Name)
            {
                ModelState.AddModelError("Studio.Name",
                    $"Current value: {dbValues.Name}");
            }
            if (dbValues.Budget != clientValues.Budget)
            {
                ModelState.AddModelError("Studio.Budget",
                    $"Current value: {dbValues.Budget:c}");
            }
            if (dbValues.StartDate != clientValues.StartDate)
            {
                ModelState.AddModelError("Studio.StartDate",
                    $"Current value: {dbValues.StartDate:d}");
            }
            if (dbValues.DirectorID != clientValues.DirectorID) {
                if (dbValues.DirectorID != null){
                    Director dbDirector = await _context.Director
                    .FindAsync(dbValues.DirectorID);
                    ModelState.AddModelError("Studio.DirectorID",
                        $"Current value: {dbDirector?.FullName}");
                } 
                else {
                    ModelState.AddModelError("Studio.DirectorID", "Current value: null");
                }
            }

            ModelState.AddModelError(string.Empty,
                "The record you attempted to edit "
              + "was modified by another user after you. The "
              + "edit operation was canceled and the current values in the database "
              + "have been displayed. If you still want to edit this record, click "
              + "the Save button again.");
        }
    }
}
