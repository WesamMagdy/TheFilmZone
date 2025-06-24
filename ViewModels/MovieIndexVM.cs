using System.ComponentModel.DataAnnotations;

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
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public Double? Rating { get; set; }
        public bool InWatchList { get; set; }
    }
}
