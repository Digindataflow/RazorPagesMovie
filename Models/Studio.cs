using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPagesMovie.Models
{
    public class Studio
    {
        public int ID { get; set; }

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

        public int? DirectorID { get; set; }
        [DisplayFormat(NullDisplayText = "No director")]
        public Director? Director { get; set; }
        [DisplayFormat(NullDisplayText = "No movies")]
        public ICollection<Movie>? Movies { get; set; }
    }
}