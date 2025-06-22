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
        public async Task<Double?> GetMovieAverageRating(int movieId)
        {
            var ratings = await userMovieRepository.GetAll()
                .Where(x => x.MovieId == movieId && x.Rating.HasValue)
                .Select(x => x.Rating.Value)
                .ToListAsync();
            if(ratings.Any()!)
            {
                return null;

            }
            return ratings.Average();
        }
        public async Task AddRating(int id , int rating)
        {
            var UserMovie = await userMovieRepository.GetByIdAsync(id);
            UserMovie.Rating = rating;
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
