using FilmZone.Models;
using Microsoft.IdentityModel.Tokens;

namespace FilmZone.Services
{
    public class CategoryService : ICategoryService
    {
        private IRepository<Category> CategoryRepo;
        private IMoviesService MoviesService;
        public CategoryService(IRepository<Category> categoryRepo, IMoviesService moviesService)
        {
            CategoryRepo = categoryRepo;
            MoviesService = moviesService;
        }
        public async Task<Category> GetById(int id)
        {
            return await CategoryRepo.GetByIdAsync(id);
        }
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            var categories = await CategoryRepo.GetAllAsync();
            return categories.OrderBy(c => c.Name);
        }
        public async Task<int> GetMoviesCount(int categoryId)
        {
            var category = await CategoryRepo.GetByIdAsync(categoryId);
            var movies = MoviesService.GetAll().Where(m => m.CategoryId == categoryId).AsQueryable();
            return movies.Count();
        }
        public async Task<Dictionary<string, string>?> SaveCategory(Category category)
        {
            var errors = new Dictionary<string, string>();
            var categoriesName = CategoryRepo.GetAll().Where(c=>c.Id!=category.Id).Select(c => c.Name.ToLower()).ToList();
            if (string.IsNullOrWhiteSpace(category.Name))
            {
                errors[nameof(CategoryViewModel.CategoryName)] = "Category Name is required";
            }
            else if (categoriesName.Contains(category.Name.ToLower()))
            {
                errors[nameof(CategoryViewModel.CategoryName)] = "Category Already Exist";
            }
            if (!category.Name.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
            {
                errors[nameof(CategoryViewModel.CategoryName)] = "Category name only letters and spaces";
            }
            if (errors.Any())
            {
                return errors;
            }
            if (category.Id == 0) //if its a new category add
            {
                await CategoryRepo.AddAsync(category);
            }
            else //Already exist update only dont add
            {
                 CategoryRepo.Update(category);   

            }
            await CategoryRepo.SaveChangesAsync();
            return null;
        }
       
       
    }
}
