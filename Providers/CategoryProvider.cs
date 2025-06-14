namespace FilmZone.Providers
{
    public class CategoryProvider
    {
        private ICategoryService CategoryService;
        public CategoryProvider(ICategoryService categoryService) 
        {
            CategoryService = categoryService;
        }
        public async Task<IEnumerable<CategoryIndexViewModel>> GetCategoryIndexView()
        {
            var categories = await CategoryService.GetAllCategoriesAsync();
            List<CategoryIndexViewModel> categoryIndexViewModels = new List<CategoryIndexViewModel>();
            foreach (var category in categories) 
            {

                categoryIndexViewModels.Add(new CategoryIndexViewModel
                {
                    CategoryId = category.Id,
                    CategoryName = category.Name,
                    MoviesCount =await CategoryService.GetMoviesCount(category.Id)
                });
            }
            return categoryIndexViewModels;

        }

    }
}
