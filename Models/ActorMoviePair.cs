using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPagesMovie.Models;

public class ActorMoviePair {

    public int Id { get; set; }
    [Required]
    public int ActorId { get; set; }
    [Required]
    public int MovieId { get; set; }

    public Movie Movie { get; set; } = default!;
    public Actor Actor { get; set; } = default!;
}