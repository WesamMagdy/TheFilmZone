using NuGet.Protocol;

namespace FilmZone.Services
{
    public class UserMovieService : IUserMoviesService
    {
        private readonly IRepository<UserMovie> userMovieRepository;
        public UserMovieService(IRepository<UserMovie> UserMovieRepository) 
        {
            userMovieRepository = UserMovieRepository;
        }
        public async Task<UserMovie> GetUserMovie(int id)
        {
          return  await userMovieRepository.GetByIdAsync(id);
        }
        public async Task<bool> IsInWatchList(string userId, int movieId)
        {
            return await userMovieRepository.GetAll()
                 .Where(x => x.MovieId == movieId && x.UserId == userId)
                 .Select(x => x.InWatchList).SingleAsync();
        }
        public async Task<Double?> GetMovieAverageRating(int movieId)
        {
            var ratings = await userMovieRepository.GetAll()
                .Where(x => x.MovieId == movieId && x.Rating.HasValue)
                .Select(x => x.Rating.Value)
                .ToListAsync();
            if(!ratings.Any())
            {
                return null;

            }
            return ratings.DefaultIfEmpty(0).Average();
        }
        // This method calculates the average rating for a movie based on user ratings.
        public async Task AssignRating(int movieid ,string userId, int rating)
        {
            var userMovie = userMovieRepository.GetAll()
                .SingleOrDefault(UM => UM.MovieId == movieid && UM.UserId == userId);
            if (userMovie != null)
            {
                userMovie.Rating = rating;
            }
            else
            {
                userMovie = new UserMovie
                {
                    MovieId = movieid,
                    UserId = userId,
                    Rating = rating,
                    InWatchList = false // or true if needed
                };
                userMovieRepository.Add(userMovie);
            }
            await userMovieRepository.SaveChangesAsync();
        }
        public async Task AddToWatchList(int id)
        {
            var userMovie = await userMovieRepository.GetByIdAsync(id);
            userMovie.InWatchList = true;
            await userMovieRepository.SaveChangesAsync();
        }
        public async Task AddReview(int id ,string review)
        {
            var userMovie = await userMovieRepository.GetByIdAsync(id);
            userMovie.Review = review;
            await userMovieRepository.SaveChangesAsync();
        }
    }
}
