using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FilmZone.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryProvider categoryProvider;
        public CategoryController(CategoryProvider categoryProvider)
        {
            this.categoryProvider = categoryProvider;
        }
            
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
                    return View(await categoryProvider.ToEditVM(Vm.CategoryId));
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
        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
