using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FilmZone.ViewModels
{
    public class CategoryViewModel
    {
        [DisplayName("ID")]
        public int CategoryId { get; set; }
        [DisplayName("Category")]
        [MaxLength(15)]
        [Required(ErrorMessage = "Category name is required")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Category Name can only contain letters")]
        public string CategoryName { get; set; }
        [DisplayName("Number Of Movies")]
        public int? MoviesCount { get; set; }
    }
}
