using System.Security.Permissions;

namespace FilmZone.Models
{
    public class StreamingService
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public ICollection<MovieStreamingService> Movies { get; set; } = new List<MovieStreamingService>();
    }
}
