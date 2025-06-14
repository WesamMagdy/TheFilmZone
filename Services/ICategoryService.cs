namespace FilmZone.Services
{
    public interface ICategoryService
    {
        public Task<IEnumerable<Category>> GetAllCategoriesAsync();
        public  Task<int> GetMoviesCount(int categoryId);

    }
}
