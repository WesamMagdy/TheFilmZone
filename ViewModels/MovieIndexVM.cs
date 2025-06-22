namespace FilmZone.ViewModels
{
    public class MovieIndexVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Cover { get; set; }
        public string Category { get; set; }
        public int? CategoryId { get; set; }
        public List<string> StreamingLogos { get; set; } = new List<string>();
        public Double? rating { get; set; }
        public bool InWatchList { get; set; }
    }
}
