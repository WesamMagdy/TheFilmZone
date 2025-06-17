using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FilmZone.Controllers
{
    [Authorize(Roles = "Admin")]

    public class CategoryController : Controller
    {
        private readonly CategoryProvider categoryProvider;
        public CategoryController(CategoryProvider categoryProvider)
        {
            this.categoryProvider = categoryProvider;
        }
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            return View(await categoryProvider.GetCategoryIndexView());
        }

        public async Task<IActionResult> Manage()
        {
            return View(await categoryProvider.GetCategoryIndexView());
        }
       // GET: CategoryController/Create
        public ActionResult Create()
        {
            var categoryViewModel = categoryProvider.CreateCategory();
            return View(categoryViewModel);
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CategoryViewModel Vm)//also used for Edit, because we use the same ViewModel for Create and Edit
        {
            var errors = await categoryProvider.AddCategory(Vm); //if there is error returning will assign to errors if not => null
            if(errors!=null)
            {
                foreach (var error in errors) { ModelState.AddModelError(error.Key, error.Value); } //add those errors in the ModelState validations
            }

            if (!ModelState.IsValid)
            {
                if(Vm.CategoryId == 0)//if its a new category
                {
                    return View(categoryProvider.CreateCategory());
                }
                else //if its an existing category we are editing
                {
                    return View(Vm);
                }
               
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: CategoryController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {

            var categoryViewModel = await categoryProvider.ToEditVM(id);
            return View(categoryViewModel);
        }
        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            var status =await categoryProvider.Delete(id);
            if (status.Success)
            {
                TempData["SuccessMessage"] = "Category Deleted Successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = status.ErrorMessage;
            }
            return RedirectToAction(nameof(Manage));
        }
    }
}
