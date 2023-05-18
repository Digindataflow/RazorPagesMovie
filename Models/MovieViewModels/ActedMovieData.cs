namespace RazorPagesMovie.Models.MovieViewModels
{
    public class ActedMovieData
    {
        public int MovieID { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool Acted { get; set; }
    }
}