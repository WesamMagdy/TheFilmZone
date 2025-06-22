using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FilmZone.ViewModels
{
    public class MovieDetailsViewModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } = String.Empty;
        public string Category { get; set; }
        public int? CategoryId { get; set; } 
        public List<string> StreamingLogos { get; set; } = new List<string>();
        public string Cover { get; set; } = default!;
        public int? MyRating { get; set; }
        public double? AvgRating { get; set; }
        public string? Review { get; set; }
        public bool InWatchList { get; set; }
    }
}
