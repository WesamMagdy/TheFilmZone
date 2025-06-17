using FilmZone.ViewModels.Identitiy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Buffers;

namespace FilmZone.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly UserProvider userProvider;
        private readonly RoleManager<IdentityRole> roleManager;
        public UserController(UserManager<ApplicationUser> userManager, UserProvider userProvider, RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.userProvider = userProvider;
        }

        public async Task<IActionResult> Index(string SearchValue)
        {
            if (SearchValue.IsNullOrEmpty())
            {
                var usersList = await userManager.Users.ToListAsync();

                var usersView = new List<UserViewModel>();
                foreach (var user in usersList)
                {
                    var userView = await userProvider.ToUserView(user);
                    usersView.Add(userView);
                }

                return View(usersView);
            }
            else
            {
                var matchedUsers = await userManager.Users
                    .Where(u => u.Email.Contains(SearchValue))
                    .ToListAsync();
                var matchedUsersView = new List<UserViewModel>();
                if (matchedUsers != null)
                {
                    foreach (var user in matchedUsers)
                    {
                        var userView = await userProvider.ToUserView(user);
                        matchedUsersView.Add(userView);
                    }
                    return View(matchedUsersView);
                }
                else
                {
                    return View(new List<UserViewModel>());
                }

            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var userView = await userProvider.ToUserView(user);
            return View(userView);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id, UserViewModel Vm)
        {
            if (ModelState.IsValid)
            {
                var updatedUser = await userProvider.UpdateFromView(Vm);
                await userManager.UpdateAsync(updatedUser);
                return RedirectToAction(nameof(Index));
            }
            return View(Vm);
        }
        [HttpGet]
        public async Task<IActionResult> AssignRole(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var Roles = roleManager.Roles.ToList();
            var AssignRoleVm = new AssignRoleViewModel
            {
                Id = id,
                Name = user.FristName + user.LastName,
                Email = user.Email,
                AvailableRoles = Roles.Select(r => new SelectListItem
                {
                    Value = r.Name,
                    Text = r.Name
                }).ToList()
            };
            return View(AssignRoleVm);
        }
        [HttpPost]
        public async Task<IActionResult> AssignRole(string id,AssignRoleViewModel Vm)
        {
            var user = await userManager.FindByIdAsync(id);
      
            if(await userManager.IsInRoleAsync(user,Vm.SelectedRole))
            {
                TempData["ErrorMessage"] = $"{Vm.SelectedRole} Is Already Assigned To {user.FristName}";
                return RedirectToAction(nameof(Index));
            }
            else
            {
               var Result= await userManager.AddToRoleAsync(user, Vm.SelectedRole);
                if(Result.Succeeded)
                    TempData["SuccessMessage"] = $"{Vm.SelectedRole} Assigned Successfully To {user.FristName}";
                return RedirectToAction(nameof(Index));

            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            await userManager.DeleteAsync(user);
            TempData["SuccessMessage"] = $"Account Successfully Deleted";
            return RedirectToAction(nameof(Index));
        }
    }
}
