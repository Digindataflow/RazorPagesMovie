using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
namespace RazorPagesMovie.Models;

public class MovieRating {
    public int ID { get; set; }

    [Display(Name = "Rating"), Range(1,5)]
    public int Star { get; set; }
    public string Comment { get; set; } = string.Empty;
    [Display(Name = "Movie")]
    public int MovieID { get; set; }
    public Movie Movie { get; set; } = null!;

}