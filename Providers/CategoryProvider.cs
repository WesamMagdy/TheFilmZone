namespace FilmZone.Providers
{
    public class CategoryProvider
    {
        private ICategoryService CategoryService;
        public CategoryProvider(ICategoryService categoryService) 
        {
            CategoryService = categoryService;
        }
       
        public async Task<IEnumerable<CategoryViewModel>> GetCategoryIndexView()
        {
            var categories = await CategoryService.GetAllCategoriesAsync();
            List<CategoryViewModel> categoryIndexViewModels = new List<CategoryViewModel>();
            foreach (var category in categories) 
            {

                categoryIndexViewModels.Add(new CategoryViewModel
                {
                    CategoryId = category.Id,
                    CategoryName = category.Name,
                    MoviesCount =await CategoryService.GetMoviesCount(category.Id)
                });
            }
            return categoryIndexViewModels;

        }
        public  CategoryViewModel CreateCategory()
        {
            return new CategoryViewModel();
        }
        public async Task AddCategory(CategoryViewModel vm)
        {
            var category = new Category
            {
                Name = vm.CategoryName
            };
           await CategoryService.SaveCategory(category);
        }
        public async Task<CategoryViewModel> ToEdit(int id)
        {
            var category = await CategoryService.GetById(id);
            return new CategoryViewModel
            {
                CategoryId = category.Id,
                CategoryName = category.Name
            };
        }
    }
}
