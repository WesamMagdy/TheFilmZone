namespace FilmZone.Providers
{
    public interface IMovieProvider
    {
        public Task<CreateMovieViewModel> GetCreateMovieVM();
        public Task<Dictionary<string, string>?> AddMovie(CreateMovieViewModel model);
        public Task<List<MovieIndexVM>> GetMovieIndexVM();
        public Task<MovieDetailsViewModel> ToDetailsVM(int id);
        public Task<CreateMovieViewModel> ToEditVM(int id);
        public  Task<Dictionary<string, string>?> EditMovie(CreateMovieViewModel ViewModel);
        public Task<bool> DeleteMovie(int id);
        public Task<List<MovieIndexVM>> GetMovieByName(string searchValue);
        public Task<List<MovieIndexVM>> GetMoviesByCategory(int CategoryId);





    }
}
