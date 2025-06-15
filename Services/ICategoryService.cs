namespace FilmZone.Services
{
    public interface ICategoryService
    {
        public Task<IEnumerable<Category>> GetAllCategoriesAsync();
        public  Task<int> GetMoviesCount(int categoryId);
        public Task<Dictionary<string, string>?> SaveCategory(Category category);
        public Task EditCategory(Category category);
        public Task<Category> GetById(int id);


    }
}
