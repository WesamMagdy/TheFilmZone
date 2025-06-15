using System.Linq;
using System.Threading.Tasks;
using FilmZone.Infrastructure;
using FilmZone.Models;

namespace FilmZone.Services
{
    public class MoviesService : IMoviesService
    {
        private IRepository<Movie> MoviesRepo;
        private IRepository<Category> CategoryRepo; //  todo: create service and use it
        private IRepository<StreamingService> StreamingRepo; //  todo: create service and use it
        private IRepository<MovieStreamingService> MoviesStreamingRepo; //  todo: create service and use it
        public MoviesService(IWebHostEnvironment webHostEnvironment,
            IRepository<Movie> moviesRepo, IRepository<Category> categoryRepo, IRepository<StreamingService> streamingRepo
            , IRepository<MovieStreamingService> MoviesStreamRepo)
        {
            MoviesRepo = moviesRepo;
            CategoryRepo = categoryRepo;
            StreamingRepo = streamingRepo;
            MoviesStreamingRepo = MoviesStreamRepo;
        }
        public async Task<Dictionary<string, string>?> AddMovie(Movie movie, long CoverSize = default)
        {
            var errors = new Dictionary<string, string>();
            List<string> allowedExtension = ["png", "jpg", "jpeg"];
            var MaxSize = 1 * 1024 * 1024;
            if (!allowedExtension.Contains(Path.GetExtension(movie.Cover).ToLowerInvariant().TrimStart('.'))) //check if file extension is in allowed
            {
                errors[nameof(CreateMovieViewModel.Cover)] = "Only png/jpg/jpeg Are Allowed!";
            }
            if (CoverSize > MaxSize) //check size is under 1MB
            {
                if (errors.ContainsKey(nameof(Movie.Cover))) // To Display both errors
                {
                    errors[nameof(CreateMovieViewModel.Cover)] += $", And Max Size Is {MaxSize / 1024 / 1024}MB";
                }
                else // only Size is the problem
                {
                    errors[nameof(CreateMovieViewModel.Cover)] = $"Max Size Is {MaxSize / 1024 / 1024}MB";

                }

            }
            if (errors.Any())
            {
                return errors;
            }
            await MoviesRepo.AddAsync(movie);
            await MoviesRepo.SaveChangesAsync();

            return null;
        }
        public async Task<Dictionary<string, string>?> EditMovie(Movie movie, long CoverSize) //if edit changes cover
        {
            var errors = new Dictionary<string, string>();
            List<string> allowedExtension = ["png", "jpg", "jpeg"];
            var MaxSize = 1 * 1024 * 1024;
            if (!allowedExtension.Contains(Path.GetExtension(movie.Cover).ToLowerInvariant().TrimStart('.'))) //check if file extension is in allowed
            {
                errors[nameof(CreateMovieViewModel.Cover)] = "Only png/jpg/jpeg Are Allowed!";
            }
            if (CoverSize > MaxSize) //check size is under 1MB
            {
                if (errors.ContainsKey(nameof(Movie.Cover))) // To Display both errors
                {
                    errors[nameof(CreateMovieViewModel.Cover)] += $", And Max Size Is {MaxSize / 1024 / 1024}MB";
                }
                else // only Size is the problem
                {
                    errors[nameof(CreateMovieViewModel.Cover)] = $"Max Size Is {MaxSize / 1024 / 1024}MB";

                }

            }
            if (errors.Any())
            {
                return errors;
            }
            await MoviesRepo.UpdateAsync(movie);
            await MoviesRepo.SaveChangesAsync();

            return null;
        }
        public async Task EditMovie(Movie movie)//if edit doesnt change cover
        {

            await MoviesRepo.UpdateAsync(movie);
            await MoviesRepo.SaveChangesAsync();

        }
        public async Task<bool> DeleteMovie(int id)
        {
            await MoviesRepo.DeleteAsyncById(id);
            await MoviesRepo.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await MoviesRepo.GetAllAsync();

        }
        public IEnumerable<Movie> GetAll()
        {
            return MoviesRepo.GetAll();
        }

        public async Task<Movie> GetMovieById(int id)
        {
            return await MoviesRepo.GetByIdAsync(id);
        }
        public async Task<List<Movie>> GetMoviesByName(string searchValue)
        {
            var movies = await MoviesRepo.GetAll().
                Where(m => m.Title.ToLower().Contains(searchValue.ToLower()))
                .ToListAsync(); //GetAll returns IQueryable
            return movies;
        }
        public async Task<Category> GetCategoryById(int id)
        {
            return await CategoryRepo.GetByIdAsync(id);
        }
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await CategoryRepo.GetAllAsync();

        }
        public async Task<IEnumerable<StreamingService>> GetAllStreamsAsync()
        {
            return await StreamingRepo.GetAllAsync();

        }

        //public async Task<List<string>> GetMovieStreamingLogos(int MovieId) //Get movie streams and select logos from them instead
        //{
        //    var jointStreamingServices = await MoviesStreamingRepo.GetAllAsync(); //get all Joint entity
        //    var Streamings = await StreamingRepo.GetAllAsync(); //Get all the Streaming Services
        //    var moviesStreamingId = jointStreamingServices.Where(ms => ms.MovieId == MovieId).Select(s => s.StreamingServiceId).ToList();
        //    return Streamings.AsQueryable()                             //get the Streaming Services with the id list and get the logo of them
        //                .Where(ss => moviesStreamingId.Contains(ss.Id))
        //                .Select(ss => ss.Logo).
        //                ToList();


        public async Task<List<StreamingService>> GetMovieStreams(int MovieId)
        {
            var jointStreamingServices = await MoviesStreamingRepo.GetAllAsync(); //get all Joint entity
            var Streamings = await StreamingRepo.GetAllAsync(); //Get all the Streaming Services
            var moviesStreamingId = jointStreamingServices.Where(ms => ms.MovieId == MovieId).Select(s => s.StreamingServiceId).ToList();
            return Streamings.Where(ss => moviesStreamingId.Contains(ss.Id)).ToList();


        }
        public async Task<IEnumerable<Movie>> GetMoviesByCategory(int categoryId)
        {
            var movies = await MoviesRepo.GetAllAsync();
            return movies.Where(m => m.CategoryId == categoryId); ;
        }
    }
}

