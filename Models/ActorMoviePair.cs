using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPagesMovie.Models;

public class ActorMoviePair {

    public int ID { get; set; }
    [Required]
    public int ActorID { get; set; }
    [Required]
    public int MovieID { get; set; }

    public Movie Movie { get; set; } = default!;
    public Actor Actor { get; set; } = default!;


    [StringLength(50)]
    public string Performance { get; set; } = string.Empty;

}