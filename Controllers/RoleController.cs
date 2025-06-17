using FilmZone.Providers;
using FilmZone.ViewModels.Identitiy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace FilmZone.Controllers
{
    [Authorize(Roles = "Admin")]

    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public async Task<ActionResult> Index()
        {
            var roles = await roleManager.Roles.ToListAsync();
            var rolesViews = new List<RoleViewModel>();
            foreach (var role in roles)
            {
                rolesViews.Add(new RoleViewModel
                {
                    Id = role.Id,
                    RoleName = role.Name
                });
            }
            return View(rolesViews);
        }
        // POST: RoleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RoleViewModel Vm)
        {
            if (ModelState.IsValid)
            {
                var Role = new IdentityRole
                {
                    Id = Vm.Id,
                    Name = Vm.RoleName,
                    NormalizedName = Vm.RoleName.ToUpper()
                };
                var RolesNames = roleManager.Roles.Select(R => R.NormalizedName);
                if (RolesNames.Contains(Role.NormalizedName))
                {
                    TempData["ErrorMessage"] = $"{Role.Name} Already Exist";
                }
                else
                {
                    var result = await roleManager.CreateAsync(Role);
                    TempData["SuccessMessage"] = $"{Role.Name} Was Added Successfully.";
                }
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Edit(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                TempData["ErrorMessage"] = "Role not found.";
                return RedirectToAction(nameof(Index));
            }
            var roleView = new RoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name,
            };
            return View(roleView);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id, RoleViewModel Vm)
        {
            if (ModelState.IsValid)
            {

                var Role = await roleManager.FindByIdAsync(id);
                Role.Name = Vm.RoleName;
                Role.NormalizedName = Vm.RoleName.ToUpper();
                var RolesNames = roleManager.Roles.Select(R => R.NormalizedName);
                if (RolesNames.Contains(Role.NormalizedName))
                {
                    TempData["ErrorMessage"] = $"{Role.Name} Already Exist";
                }
                else
                {
                    var result = await roleManager.UpdateAsync(Role);
                    TempData["SuccessMessage"] = $"{Role.Name} Was Edited Successfully.";
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Vm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var Role = await roleManager.FindByIdAsync(id);
            await roleManager.DeleteAsync(Role);
            TempData["SuccessMessage"] = $"{Role.Name} Successfully Deleted";
            return RedirectToAction(nameof(Index));
        }
    }
}
