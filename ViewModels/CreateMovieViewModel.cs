using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FilmZone.ViewModels
{
    public class CreateMovieViewModel
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Title { get; set; }
        [MaxLength(2500)]
        public string Description { get; set; } =String.Empty;
        [DisplayName("Category")]
        public int CategoryId { get; set; }
       
        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
        [DisplayName("Streaming Service")]
        [Required]
        public List<int> SelectedStreamingServiceId { get; set; } = new List<int>();
            
        public IEnumerable<SelectListItem> StreamingServices = Enumerable.Empty<SelectListItem>();
        public IFormFile? Cover { get; set; } = default!;
        public string? CoverName { get; set; }
    }
}
