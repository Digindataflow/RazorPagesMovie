
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPagesMovie.Models.StudioViewModels;

public class StudioViewModel
{

    [StringLength(50, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;

    [DataType(DataType.Currency)]
    // change data type mapping
    [Column(TypeName = "money")]
    public decimal Budget { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
                    ApplyFormatInEditMode = true)]
    [Display(Name = "Start Date")]
    public DateTime StartDate { get; set; }
    public Guid ConcurrencyToken { get; set; } = Guid.NewGuid();

    [DisplayFormat(NullDisplayText = "No director"), Display(Name = "Director")]
    public int? DirectorID { get; set; }
}