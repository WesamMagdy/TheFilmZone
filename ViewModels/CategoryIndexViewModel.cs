using System.ComponentModel;

namespace FilmZone.ViewModels
{
    public class CategoryIndexViewModel
    {
        [DisplayName("ID")]
        public int? CategoryId { get; set; }
        [DisplayName("Category")]
        public string CategoryName { get; set; }
        [DisplayName("Number Of Movies")]
        public int MoviesCount { get; set; }
    }
}
