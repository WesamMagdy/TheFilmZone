namespace FilmZone.Services
{
    public interface IMoviesService
    {
        public Task<Dictionary<string, string>?> AddMovie(Movie movie, long CoverSize);
        public  Task<Dictionary<string, string>?> EditMovie(Movie movie, long CoverSize);
        public Task EditMovie(Movie movie); //if no cover is uploaded and no extra validation needed

        public Task<IEnumerable<Movie>> GetAllAsync();
        public IEnumerable<Movie> GetAll();
        public  Task<Movie> GetMovieById(int id);
        public  Task<Category> GetCategoryById(int id);
        public Task<IEnumerable<Category>> GetAllCategoriesAsync();
        public Task<List<StreamingService>> GetMovieStreams(int MovieId);
        public Task<IEnumerable<StreamingService>> GetAllStreamsAsync();
        public Task<bool> DeleteMovie(int id);
        public Task<List<Movie>> GetMoviesByName(string searchValue);
        public  Task<IEnumerable<Movie>> GetMoviesByCategory(int categoryId);













    }
}
