using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesMovie.Models
{
    public class Actor
    {
        public int ID { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string LastName { get; set; } = default!;

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string FirstMidName { get; set; }= default!;

        [Display(Name = "Birth Date"), DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public ICollection<ActorMoviePair> ActorMoviePairs { get; set; } = default!;

    }
}