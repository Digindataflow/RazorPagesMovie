using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesMovie.Models
{
    public class Actor
    {
        public int ID { get; set; }

        [StringLength(60, MinimumLength = 3), Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; } = string.Empty;

        [StringLength(60, MinimumLength = 3, ErrorMessage = "First name cannot be longer than 60 characters.")]
        [Display(Name = "First Name")]
        [Required]
        public string FirstMidName { get; set; }= string.Empty;

        [Display(Name = "Birth Date"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstMidName;
            }
        }

        public ICollection<ActorMoviePair> ActorMoviePairs { get; set; } = default!;

    }
}