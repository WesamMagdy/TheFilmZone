using System.Diagnostics;
using FilmZone.Models;
using Microsoft.AspNetCore.Mvc;
using FilmZone.Providers;
using Microsoft.AspNetCore.Authorization;

namespace FilmZone.Controllers
{
    public class HomeController : Controller
        
    {
        private readonly ILogger<HomeController> _logger;
        private IMovieProvider MovieProvider;

        public HomeController(ILogger<HomeController> logger, IMovieProvider movieProvider)
        {
            _logger = logger;
            this.MovieProvider = movieProvider;
        }

        public async Task<IActionResult> Index(string searchValue, int id = 0)
        {
            List<MovieIndexVM> MovieIndexVm;
            if (!string.IsNullOrEmpty(searchValue))
            {
                MovieIndexVm = await MovieProvider.GetMovieByName(searchValue);

            }
            else if (id != 0)
            {
                MovieIndexVm = await MovieProvider.GetMoviesByCategory(id);
            }
            else
            {
                MovieIndexVm = await MovieProvider.GetMovieIndexVM();
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
