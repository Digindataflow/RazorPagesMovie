using System;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesMovie.Models.MovieViewModels
{
    public class ActorMovieDateGroup
    {
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        public int ActorCount { get; set; }
    }
}