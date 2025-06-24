using FilmZone.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace FilmZone.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<StreamingService> StreamingServices { get; set; }
        public DbSet<MovieStreamingService> MovieStreamingServices { get; set; }
        public DbSet<UserMovie> UserMovies { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MovieStreamingService>()
                 .HasKey(mss => new { mss.MovieId, mss.StreamingServiceId });
            // Configure your entity mappings here
            modelBuilder.Entity<MovieStreamingService>()
           .HasOne(mss => mss.movie)
           .WithMany(m => m.streamingServices)
           .HasForeignKey(mss => mss.MovieId)
           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserMovie>()
                .HasKey(UM => UM.Id); //primary key for UserMovie
            modelBuilder.Entity<UserMovie>()
                .HasIndex(UM => new { UM.UserId, UM.MovieId })
                .IsUnique();//unique index to prevent duplicate entries for the same user and movie
            modelBuilder.Entity<UserMovie>() //the many to many relationship between User and Movie
                .HasOne(u=>u.User)
                .WithMany() 
                .HasForeignKey(UM => UM.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UserMovie>()
                    .HasOne(m=>m.Movie)
                    .WithMany()
                    .HasForeignKey(UM => UM.MovieId)
                    .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<Category>()
                .HasMany(c => c.Movies)
                .WithOne(m => m.Category)
                .HasForeignKey(m => m.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);
                 modelBuilder.Entity<Category>()
                .HasData(new Category[]
                {
                    new Category { Id = 1, Name = "Action" },
                    new Category { Id = 2, Name = "Comedy" },
                    new Category { Id = 3, Name = "Horror" },
                    new Category { Id = 4, Name = "Drama" },
                    new Category { Id = 5, Name = "Fantasy" },
                    new Category { Id = 6, Name = "Science Fiction" },
                    new Category { Id = 7, Name = "Romance" },
                    new Category { Id = 8, Name = "Thriller" },
                    new Category { Id = 9, Name = "Documentary" },
                    new Category { Id = 10, Name = "Animation" }
                });
            
            modelBuilder.Entity<StreamingService>()
                 .HasData(new StreamingService[]
                 {
                        new StreamingService { Id = 1 ,Name = "Netflix",Logo= "NetflixLogo" },
                        new StreamingService { Id = 2 ,Name = "Amazon Prime",Logo= "AmazonPrimeLogo" },
                        new StreamingService { Id = 3 ,Name = "Disney+",Logo= "DisneyPlusLogo" },
                        new StreamingService { Id = 4 ,Name = "Hulu",Logo= "HuluLogo" },
                        new StreamingService { Id = 5 ,Name = "HBO Max",Logo= "HBOMaxLogo" },
                        new StreamingService { Id = 6 ,Name = "Apple TV+",Logo= "AppleTVPlusLogo" },
                        new StreamingService { Id = 7 ,Name = "WatchIt",Logo= "WatchItLogo" },
                        new StreamingService { Id = 8 ,Name = "Shahid",Logo= "ShahidLogo" },

                 });

        }


    }
}

