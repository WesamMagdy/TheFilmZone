namespace FilmZone.Services
{
    public class CategoryService : ICategoryService
    {
        private IRepository<Category> CategoryRepo;
        private IMoviesService MoviesService;
        public CategoryService(IRepository<Category> categoryRepo,IMoviesService moviesService) 
        {
            CategoryRepo = categoryRepo;
            MoviesService = moviesService;
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
    }
}
