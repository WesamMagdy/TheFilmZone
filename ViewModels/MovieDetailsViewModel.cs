using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FilmZone.ViewModels
{
    public class MovieDetailsViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; } = String.Empty;
        public string Category { get; set; }
        public List<string> StreamingLogos { get; set; } = new List<string>();
        public string Cover { get; set; } = default!;
    }
}
