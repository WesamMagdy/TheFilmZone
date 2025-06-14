    namespace FilmZone.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Cover { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public ICollection<MovieStreamingService> streamingServices { get; set; } = new List<MovieStreamingService>();

    }
}
