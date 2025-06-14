namespace FilmZone.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Movie> Movies = new List<Movie>();

    }
}
