using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPagesMovie.Models
{
    public class Home
    {

        public int ID { get; set; }
        public int DirectorID { get; set; }
        [StringLength(50)]
        [Display(Name = "Home Location")]
        public string Location { get; set; } = string.Empty;

        public Director Director { get; set; } = default!;
    }
}