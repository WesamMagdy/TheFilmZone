﻿using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Threading.Tasks;
using System.Security.Claims;
namespace FilmZone.Controllers
{
    [Authorize(Roles = "Admin")]

    public class MoviesController : Controller
    {

        private readonly MovieProvider MovieProvider;
       
        public MoviesController(MovieProvider movieProvider)
        {
            MovieProvider = movieProvider;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string SearchValue)
        {
            List<MovieIndexVM> MovieIndexVm;    
            if (!string.IsNullOrEmpty(SearchValue))
            {
                MovieIndexVm = await MovieProvider.GetMovieByName(SearchValue);

            }
            else
            {
                MovieIndexVm = await MovieProvider.GetMovieIndexVM();
            }
            return View(MovieIndexVm);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id) 
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var DetailsVM =await MovieProvider.ToDetailsVM(userId,id);
            return View(DetailsVM);
        }
        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddRating(int MovieId,string UserId,int Rating)
        {
           await MovieProvider.AddRating(MovieId, UserId, Rating);
            TempData["SuccessMessage"] = "Rating Added Successfully.";
           return RedirectToAction(nameof(Details), new { id = MovieId });

        }
        [HttpGet]
        public async Task<IActionResult> Create()
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
            else
            {
                TempData["SuccessMessage"] = $"{ViewModel.Title} Was Added Successfully.";

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
            else
                TempData["SuccessMessage"] = $"{MovieViewModel.Title} Edited Successfully.";

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
            var movieName = await MovieProvider.GetMovieName(id);
            if (IsDeleted)
            {
                TempData["SuccessMessage"] = $"{movieName} deleted Successfully.";

            }
            return RedirectToAction(nameof(Index));

        }
    }
}

