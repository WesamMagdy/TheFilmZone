using FilmZone.Models;
using FilmZone.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace FilmZone.Controllers
{
    public class HomeController : Controller
        
    {
        private readonly ILogger<HomeController> _logger;
        private MovieProvider MovieProvider;

        public HomeController(ILogger<HomeController> logger, MovieProvider movieProvider)
        {
            _logger = logger;
            this.MovieProvider = movieProvider;
        }

        public async Task<IActionResult> Index(string searchValue, int categoryId = 0, bool watchList=false)
        {
            List<MovieIndexVM> MovieIndexVm;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(searchValue))
            {
                MovieIndexVm = await MovieProvider.GetMovieByName(userId,searchValue);

            }
            else if (categoryId != 0)
            {
                MovieIndexVm = await MovieProvider.GetMoviesByCategory(userId, categoryId);
            }
            else if (watchList == true)
            {
                MovieIndexVm = await MovieProvider.GetMoviesInWatchList(userId);
            }
            else
            {
                MovieIndexVm = await MovieProvider.GetMovieIndexVM(userId);
            }
            return View(MovieIndexVm);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
