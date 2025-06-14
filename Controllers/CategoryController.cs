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
        public async Task<ActionResult> Create(CategoryViewModel Vm)
        {
            if (!ModelState.IsValid)
            {
                var ViewModel = categoryProvider.CreateCategory();
                return View(ViewModel);
            }
            await categoryProvider.AddCategory(Vm);
            return RedirectToAction(nameof(Index));
        }

        // GET: CategoryController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {

            var categoryViewModel = await categoryProvider.ToEdit(id);
            return View(categoryViewModel);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryViewModel categoryViewModel)
        {
            return View();
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
