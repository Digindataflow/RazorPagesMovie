using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPagesMovie.Models;

public class Movie
{
    public int ID { get; set; }

    [StringLength(60, MinimumLength = 3)]
    [Required]
    public string Title { get; set; } = string.Empty;

    // specifies the display name of a field.
    [Display(Name = "Release Date")]
    // specifies the type of the data
    // The browser can enable HTML5 features,
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }

    
    [Range(1, 100)]
    [DataType(DataType.Currency)]
    // map Price to currency in the database.
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }


    [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
    [Required]
    [StringLength(30)]
    public string Genre { get; set; } = string.Empty;

    [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
    [StringLength(5)]
    [Required]
    public string Rating { get; set; } = string.Empty;

    [Required]
    public ICollection<ActorMoviePair> ActorMoviePairs { get; set; } = new List<ActorMoviePair>();

    // If FK property StudioID is not included, 
    // The Studio property is null if it's not explicitly loaded when Movie is fetched. 
    // To update the Movie entity, the Studio entity must first be fetched.
    public int StudioID { get; set; }
    public Studio Studio { get; set; } = default!;
    public ICollection<Director> Directors { get; set; } = new List<Director>();

    public MovieRating? Star { get; set; }
}