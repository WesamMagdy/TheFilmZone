using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FilmZone.ViewModels
{
    public class CategoryViewModel
    {
        [DisplayName("ID")]
        public int? CategoryId { get; set; }
        [DisplayName("Category")]
        [MaxLength(15)]
        public string CategoryName { get; set; }
        [DisplayName("Number Of Movies")]
        public int? MoviesCount { get; set; }
    }
}
