using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FilmZone.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieProvider MovieProvider;
        public MoviesController(IMovieProvider movieProvider)
        {
            MovieProvider = movieProvider;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var MovieIndexVm = await MovieProvider.GetMovieIndexVM();
            return View(MovieIndexVm);
        }
        public async Task<IActionResult> Details(int id) 
        {
            var DetailsVM =await MovieProvider.ToDetailsVM(id);
            return View(DetailsVM);
        }
        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            var VM = await MovieProvider.GetCreateMovieVM();
            return View(VM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMovieViewModel ViewModel)
        {
            var errors = await MovieProvider.AddMovie(ViewModel); // Will add errors without saving if there is errors,save and add no errors if correct
            if (errors != null) 
            {
                foreach (var error in errors) {ModelState.AddModelError(error.Key, error.Value); }
            }
            if (ViewModel.Cover == null)
            {
                ModelState.AddModelError("Cover", "Cover is required.");
            }

            if (!ModelState.IsValid)
            {
                ViewModel = await MovieProvider.GetCreateMovieVM();
                return View(ViewModel);
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var movie = await MovieProvider.ToEditVM(id);
            return View(movie);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateMovieViewModel MovieViewModel)
        {
            var errors = await MovieProvider.EditMovie(MovieViewModel);// Will add errors without saving if there is errors,save the edit if correct
            if (errors != null)
            {
                foreach (var error in errors) { ModelState.AddModelError(error.Key, error.Value); }
            }
            if (!ModelState.IsValid)
            {
                MovieViewModel = await MovieProvider.ToEditVM(MovieViewModel.Id);
                return View(MovieViewModel);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var IsDeleted =await MovieProvider.DeleteMovie(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

