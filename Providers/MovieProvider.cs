using System.Xml.Schema;
using FilmZone.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.LibraryModel;
using System.Security.Claims;


namespace FilmZone.Providers
{
    public class MovieProvider
    {
        private IMoviesService MoviesService;
        private readonly IUserMoviesService UserMovieService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public MovieProvider(IMoviesService moviesService, IUserMoviesService userMoviesService, IWebHostEnvironment webHostEnvironment)
        {
            MoviesService = moviesService;
            UserMovieService = userMoviesService;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<CreateMovieViewModel> GetCreateMovieVM()
        {
            var Categories = await MoviesService.GetAllCategoriesAsync();
            var StreamingServices = await MoviesService.GetAllStreamsAsync();
            return new CreateMovieViewModel
            {
                Categories = Categories.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }).OrderBy(c => c.Text),
                StreamingServices = StreamingServices.Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                }).OrderBy(s => s.Text)
            };
        }
        public async Task<Dictionary<string, string>?> AddMovie(CreateMovieViewModel ViewModel)
        {
            var coverFileName = $"{Guid.NewGuid()}{Path.GetExtension(ViewModel.Cover.FileName)}";
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "images", "Movies", coverFileName);
            using var stream = File.Create(path);
            await ViewModel.Cover.CopyToAsync(stream);

            var Movie = new Movie
            {
                Title = ViewModel.Title,
                Description = ViewModel.Description,
                CategoryId = ViewModel.CategoryId,
                Cover = coverFileName,
                streamingServices = ViewModel.SelectedStreamingServiceId.Select(d => new MovieStreamingService { StreamingServiceId = d }).ToList()

            };
            return await MoviesService.AddMovie(Movie, ViewModel.Cover.Length);
        }
        public async Task<List<MovieIndexVM>> GetMovieIndexVM(string? userId)
        {
            var movies = await MoviesService.GetAllAsync();//get all the movies from database using repo
            List<MovieIndexVM> moviesIndexVM = new List<MovieIndexVM>();
            foreach (var movie in movies)
            {
                var Streams = await MoviesService.GetMovieStreams(movie.Id);
                var category = await MoviesService.GetCategoryById(movie.CategoryId);
                var rating = await UserMovieService.GetMovieAverageRating(movie.Id);
                var isInWatchlist = await UserMovieService.IsInWatchList(userId, movie.Id);
                moviesIndexVM.Add(new MovieIndexVM
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Cover = movie.Cover,
                    Category = category.Name,
                    CategoryId = category.Id,
                    StreamingLogos = Streams.Select(s => s.Logo).ToList(),
                    Rating = rating,
                    InWatchList = isInWatchlist

                });
            }
            return moviesIndexVM;

        }

        public async Task<List<MovieIndexVM>> GetMovieByName(string? userId, string searchValue)
        {
            var movies = await MoviesService.GetMoviesByName(searchValue);
            List<MovieIndexVM> moviesIndexVM = new List<MovieIndexVM>();
            foreach (var movie in movies)
            {
                var Streams = await MoviesService.GetMovieStreams(movie.Id);
                var category = await MoviesService.GetCategoryById(movie.CategoryId);
                var rating = await UserMovieService.GetMovieAverageRating(movie.Id);
                var isInWatchlist = await UserMovieService.IsInWatchList(userId, movie.Id);
                moviesIndexVM.Add(new MovieIndexVM
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Cover = movie.Cover,
                    Category = category.Name,
                    CategoryId = category.Id,
                    StreamingLogos = Streams.Select(s => s.Logo).ToList(),
                    Rating = rating,
                    InWatchList = isInWatchlist

                });
            }
            return moviesIndexVM;

        }
        public async Task<List<MovieIndexVM>> GetMoviesByCategory(string? userId, int CategoryId)
        {
            var movies = await MoviesService.GetMoviesByCategory(CategoryId);//get movies with given category
            List<MovieIndexVM> moviesIndexVM = new List<MovieIndexVM>();
            foreach (var movie in movies)
            {
                var Streams = await MoviesService.GetMovieStreams(movie.Id);
                var category = await MoviesService.GetCategoryById(movie.CategoryId);
                var rating = await UserMovieService.GetMovieAverageRating(movie.Id);
                var isInWatchlist = await UserMovieService.IsInWatchList(userId, movie.Id);
                moviesIndexVM.Add(new MovieIndexVM
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Cover = movie.Cover,
                    Category = category.Name,
                    CategoryId = category.Id,
                    StreamingLogos = Streams.Select(s => s.Logo).ToList(),
                    Rating = rating,
                    InWatchList = isInWatchlist
                });
            }
            return moviesIndexVM;

        }
        public async Task<List<MovieIndexVM>> GetMoviesInWatchList(string? userId)
        {
            var moviesId = UserMovieService.GetMoviesIdInWatchList(userId).ToList();
            var movies = MoviesService.GetAll().Where(m => moviesId.Contains(m.Id)).ToList();
            List<MovieIndexVM> moviesIndexVM = new List<MovieIndexVM>();
            foreach (var movie in movies)
            {
                var Streams = await MoviesService.GetMovieStreams(movie.Id);
                var category = await MoviesService.GetCategoryById(movie.CategoryId);
                var rating = await UserMovieService.GetMovieAverageRating(movie.Id);
                var isInWatchlist = await UserMovieService.IsInWatchList(userId, movie.Id);
                moviesIndexVM.Add(new MovieIndexVM
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Cover = movie.Cover,
                    Category = category.Name,
                    CategoryId = category.Id,
                    StreamingLogos = Streams.Select(s => s.Logo).ToList(),
                    Rating = rating,
                    InWatchList = isInWatchlist
                });
            }
            return moviesIndexVM;

        }
        public async Task<MovieDetailsViewModel> ToDetailsVM(string? userId, int MovieId)
        {
            var movie = await MoviesService.GetMovieById(MovieId);
            var category = await MoviesService.GetCategoryById(movie.CategoryId);
            var Streams = await MoviesService.GetMovieStreams(MovieId);
            var rating = await UserMovieService.GetMovieAverageRating(MovieId);
            var myRating = await UserMovieService.GetMyRating(userId, MovieId);
            var isInWatchlist = await UserMovieService.IsInWatchList(userId, movie.Id);
            return new MovieDetailsViewModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Category = category.Name,
                CategoryId = category.Id,
                Description = movie.Description,
                Cover = movie.Cover,
                StreamingLogos = Streams.Select(s => s.Logo).ToList(),
                Rating = rating,
                MyRating = myRating,
                InWatchList = isInWatchlist
            };

        }
        public async Task<CreateMovieViewModel> ToEditVM(int id)
        {
            var movie = await MoviesService.GetMovieById(id);
            var Categories = await MoviesService.GetAllCategoriesAsync();
            var StreamingServices = await MoviesService.GetAllStreamsAsync();
            var OldStreams = await MoviesService.GetMovieStreams(id);
            return new CreateMovieViewModel
            {
                Id = id,
                Title = movie.Title,
                Description = movie.Description,
                CoverName = movie.Cover,//takes the name from db to be able to preview the old cover
                CategoryId = movie.CategoryId,
                SelectedStreamingServiceId = OldStreams.Select(os => os.Id).ToList(),
                Categories = Categories.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }).OrderBy(c => c.Text),
                StreamingServices = StreamingServices.Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                }).OrderBy(s => s.Text)
            };

        }
        public async Task<Dictionary<string, string>?> EditMovie(CreateMovieViewModel ViewModel)
        {
            var movieStreams = await MoviesService.GetMovieStreams(ViewModel.Id);
            var movie = await MoviesService.GetMovieById(ViewModel.Id);
            movie.Title = ViewModel.Title;
            movie.Description = ViewModel.Description;
            movie.CategoryId = ViewModel.CategoryId;
            movie.streamingServices = ViewModel.SelectedStreamingServiceId.Select(SS => new MovieStreamingService { MovieId = ViewModel.Id, StreamingServiceId = SS }).ToList();
            bool HasNewCover = ViewModel.Cover != null; //check if new cover is uploaded
            if (HasNewCover)
            {
                // Delete old cover if it exists
                if (!string.IsNullOrEmpty(movie.Cover))
                {
                    var oldCoverPath = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "images", "Movies", movie.Cover);
                    if (File.Exists(oldCoverPath))
                    {
                        File.Delete(oldCoverPath);
                    }
                }

                var coverFileName = $"{Guid.NewGuid()}{Path.GetExtension(ViewModel.Cover.FileName)}";
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "images", "Movies", coverFileName);
                using var stream = File.Create(path);
                await ViewModel.Cover.CopyToAsync(stream);
                movie.Cover = coverFileName;
                return await MoviesService.EditMovie(movie, ViewModel.Cover.Length);
            }
            await MoviesService.EditMovie(movie);
            return null;
        }
        public async Task<bool> DeleteMovie(int id)
        {

            var movie = await MoviesService.GetMovieById(id);
            if (movie is null) return false;
            var oldCoverPath = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "images", "Movies", movie.Cover);
            File.Delete(oldCoverPath);
            return await MoviesService.DeleteMovie(id);
        }
        public async Task<string> GetMovieName(int id)
        {
            var movie = await MoviesService.GetMovieById(id);
            return movie.Title;
        }
        public async Task AddRating(int MovieId, string UserId, int Rating)
        {
            await UserMovieService.AssignRating(MovieId, UserId, Rating);
        }
        public async Task ToggleWatchList(string userId, int movieId)
        {
            if (await UserMovieService.IsInWatchList(userId, movieId))
            {
                await UserMovieService.RemoveFromWatchList(userId, movieId);
            }
            else
                await UserMovieService.AddToWatchList(userId, movieId);
        }
    }

}
