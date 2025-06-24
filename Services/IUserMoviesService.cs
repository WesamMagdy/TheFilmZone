namespace FilmZone.Services
{
    public interface IUserMoviesService
    {
        public  Task<UserMovie> GetUserMovie(int id);
        public  Task AssignRating(int movieid, string userId, int rating);
        public Task AddToWatchList(int id);
        public Task AddReview(int id, string review);
        public  Task<Double?> GetMovieAverageRating(int movieId);
        public  Task<bool> IsInWatchList(string userId, int movieId);


    }
}
