namespace FilmZone.Models
{
    public class MovieStreamingService
    {
        public int MovieId { get; set; }
        public Movie movie { get; set; }
        public int StreamingServiceId { get; set; }
        public StreamingService streamingService { get; set; }

    }
}
