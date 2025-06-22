namespace FilmZone.Models
{
    public class UserMovie
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int? Rating { get; set; }
        public string? Review { get; set; }
        public bool InWatchList { get; set; }

    }
}
