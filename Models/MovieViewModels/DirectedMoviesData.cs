namespace RazorPagesMovie.Models.MovieViewModels
{
    public class DirectedMoviesData
    {
        public int MovieID { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool Directed { get; set; }
    }
}