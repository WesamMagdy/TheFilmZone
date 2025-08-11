namespace FilmZone.Services
{
    public interface IUserMoviesService
    {
        public  Task<UserMovie> GetUserMovie(string userId,int moiveId);
        public  Task AssignRating(int movieid, string userId, int rating);
        public Task AddToWatchList(string userid, int movieId);
        public Task RemoveFromWatchList(string userid, int movieId);
        public Task AddReview(int id, string review);
        public  Task<Double?> GetMovieAverageRating(int movieId);
        public  Task<bool> IsInWatchList(string userId, int movieId);
        public Task<int?> GetMyRating(string userId, int movieId);
        public IEnumerable<int> GetMoviesIdInWatchList(string userId);






    }
}
