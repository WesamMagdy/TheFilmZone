using Microsoft.AspNetCore.Identity;

namespace FilmZone.Infrastructure.Seeding
{
    public static class DataSeeding
    {
 
        public static async Task SeedMovies(ApplicationDbContext _context)
        {
            if (!await _context.Movies.AnyAsync())
            {
 
                var movies = new List<Movie>
                {
                    new Movie {
                        Title = "Inception",
                        Description = "A mind-bending thriller about dreams within dreams.",
                        CategoryId = 1,
                        Cover = "inception.jpg"
                    },
                    new Movie {
                        Title = "The Godfather",
                        Description = "A classic crime film about the Italian-American mafia.",
                        CategoryId = 4,
                        Cover = "TheGodfather.jpg"
                    },
                    new Movie {
                        Title = "The Dark Knight",
                        Description = "A superhero film featuring Batman and the Joker.",
                        CategoryId = 1,
                        Cover = "TheDarkKnight.jpg"
                    },
                    new Movie {
                        Title = "The Shawshank Redemption",
                        Description = "A story of hope and friendship inside a prison.",
                        CategoryId = 4,
                        Cover = "ShawshankRedemption.jpg"
                    },
                    new Movie {
                        Title = "Interstellar",
                        Description = "A sci-fi epic about space exploration and time relativity.",
                        CategoryId = 6,
                        Cover = "Interstellar.jpg"
                    },
                    new Movie {
                        Title = "Forrest Gump",
                        Description = "The life journey of a kind-hearted and simple man.",
                        CategoryId = 2,
                        Cover = "ForrestGump.jpg"
                    },
                    new Movie {
                        Title = "Fight Club",
                        Description = "An underground club and a descent into chaos.",
                        CategoryId = 8,
                        Cover = "FightClub.jpg"
                    },
                    new Movie {
                        Title = "Gladiator",
                        Description = "A Roman general seeks vengeance as a gladiator.",
                        CategoryId = 4,
                        Cover = "Gladiator.jpg"
                    },
                    new Movie {
                        Title = "The Matrix",
                        Description = "A hacker discovers a simulated reality and fights for freedom.",
                        CategoryId = 6,
                        Cover = "TheMatrix.jpg"
                    }
                };
                await _context.Movies.AddRangeAsync(movies);
                if (_context.ChangeTracker.HasChanges())
                {
                    await _context.SaveChangesAsync();
                }
            }
        }
        public static async Task SeedMoviesStreams(ApplicationDbContext _context)
        {
            if (!await _context.MovieStreamingServices.AnyAsync())
            {


                var movieStreams = new List<MovieStreamingService>
            {
                // Inception
                new MovieStreamingService { MovieId = 1, StreamingServiceId = 2 },
                new MovieStreamingService { MovieId = 1, StreamingServiceId = 8 },
                new MovieStreamingService { MovieId = 1, StreamingServiceId = 4 },
                new MovieStreamingService { MovieId = 1, StreamingServiceId = 1 },

                // The Godfather
                new MovieStreamingService { MovieId = 2, StreamingServiceId = 4 },
                new MovieStreamingService { MovieId = 2, StreamingServiceId = 1 },
                new MovieStreamingService { MovieId = 2, StreamingServiceId = 5 },

                // The Dark Knight
                new MovieStreamingService { MovieId = 3, StreamingServiceId = 1 },
                new MovieStreamingService { MovieId = 3, StreamingServiceId = 3 },
                new MovieStreamingService { MovieId = 3, StreamingServiceId = 2 },
                new MovieStreamingService { MovieId = 3, StreamingServiceId = 6 },
                                // The Shawshank Redemption

                new MovieStreamingService { MovieId = 4, StreamingServiceId = 4 },
                new MovieStreamingService { MovieId = 4, StreamingServiceId = 5 },
                                // Interstellar

                new MovieStreamingService { MovieId = 5, StreamingServiceId = 2 },
                new MovieStreamingService { MovieId = 5, StreamingServiceId = 5 },
                new MovieStreamingService { MovieId = 5, StreamingServiceId = 7 },
                                // Forrest Gump

                new MovieStreamingService { MovieId = 6, StreamingServiceId = 8 },
                new MovieStreamingService { MovieId = 6, StreamingServiceId = 2 },
                                // Fight Club

                new MovieStreamingService { MovieId = 7, StreamingServiceId = 1 },
                new MovieStreamingService { MovieId = 7, StreamingServiceId = 3 },
                new MovieStreamingService { MovieId = 7, StreamingServiceId = 5 },
                                // Gladiator

                new MovieStreamingService { MovieId = 8, StreamingServiceId = 6 },
                new MovieStreamingService { MovieId = 8, StreamingServiceId = 1 },
                new MovieStreamingService { MovieId = 8, StreamingServiceId = 4 },
                //The Matrix
                new MovieStreamingService { MovieId = 9, StreamingServiceId = 5 },
                new MovieStreamingService { MovieId = 9, StreamingServiceId = 7 },
                new MovieStreamingService { MovieId = 9, StreamingServiceId = 1 },

              
            };
                await _context.MovieStreamingServices.AddRangeAsync(movieStreams);
                if (_context.ChangeTracker.HasChanges())
                {
                    await _context.SaveChangesAsync();
                }
            }
        }
        public static async Task SeedAdminUserAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string roleName = "Admin";
            string email = "admin@filmZone.com";
            string password = "Admin123!";

            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    FristName = "admin",
                    LastName = "",
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
                else
                {
                    // Optional: log or throw if user creation fails
                    throw new Exception("Failed to create seed user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
            else
            {
                // Ensure the user is in the role
                if (!await userManager.IsInRoleAsync(user, roleName))
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }

    }
}
//public static async Task SeedAsync(StoreContext context)
//{
//    if (!context.ProductBrands.Any())
//    {
//        var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
//        var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
//        context.ProductBrands.AddRange(brands);
//    }
//    if (!context.ProductTypes.Any())
//    {
//        var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
//        var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
//        context.ProductTypes.AddRange(types);
//    }
//    if (!context.Products.Any())
//    {
//        var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
//        var products = JsonSerializer.Deserialize<List<Product>>(productsData);
//        context.Products.AddRange(products);
//    }
   
//}