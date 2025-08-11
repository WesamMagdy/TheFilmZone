using FilmZone.Models;
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
        public async Task<UserMovie?> GetUserMovie(string userid, int movieId)
        {
            return await userMovieRepository.GetAll().
                Where(um => um.UserId == userid && um.MovieId == movieId).
                SingleOrDefaultAsync();
        }
        public async Task<bool> IsInWatchList(string userId, int movieId)
        {
            return await userMovieRepository.GetAll()
                 .Where(x => x.MovieId == movieId && x.UserId == userId)
                 .Select(x => x.InWatchList).SingleOrDefaultAsync();
        }
        public async Task<Double?> GetMovieAverageRating(int movieId)
        {
            var ratings = await userMovieRepository.GetAll()
                .Where(x => x.MovieId == movieId && x.Rating.HasValue)
                .Select(x => x.Rating.Value)
                .ToListAsync();
            if (!ratings.Any())
            {
                return null;

            }
            return ratings.DefaultIfEmpty(0).Average();
        }
        public async Task<int?> GetMyRating(string userId, int movieId)
        {
            return await userMovieRepository.GetAll().Where(um => um.UserId == userId && um.MovieId == movieId)
                .Select(um => um.Rating.Value).SingleOrDefaultAsync();
        }
        public async Task AssignRating(int movieid, string userId, int rating)
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
        public async Task AddToWatchList(string userid, int movieId)
        {
            var userMovie = await GetUserMovie(userid, movieId);
            if (userMovie is not null)
            {
                userMovie.InWatchList = true;
                await userMovieRepository.SaveChangesAsync();
            }
        }
        public async Task RemoveFromWatchList(string userid, int movieId)
        {
            var userMovie = await GetUserMovie(userid, movieId);
            if (userMovie is not null)
            {
                userMovie.InWatchList = false   ;
                await userMovieRepository.SaveChangesAsync();
            }
        }
        public  IEnumerable<int> GetMoviesIdInWatchList(string userId)
        {
            return  userMovieRepository.GetAll().Where(m => m.UserId == userId && m.InWatchList==true).Select(m=>m.MovieId) ;
        }
        public async Task AddReview(int id, string review)
        {
            var userMovie = await userMovieRepository.GetByIdAsync(id);
            userMovie.Review = review;
            await userMovieRepository.SaveChangesAsync();
        }
    }
}
