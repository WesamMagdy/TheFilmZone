using FilmZone.ViewModels.Identitiy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Buffers;

namespace FilmZone.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly UserProvider userProvider;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public UserController(UserManager<ApplicationUser> userManager, UserProvider userProvider, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.userProvider = userProvider;
            this.signInManager = signInManager;
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
        public async Task<IActionResult> AssignRole(string id, AssignRoleViewModel Vm)
        {
            var user = await userManager.FindByIdAsync(id);

            if (await userManager.IsInRoleAsync(user, Vm.SelectedRole))
            {
                TempData["ErrorMessage"] = $"{Vm.SelectedRole} Is Already Assigned To {user.FristName}";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var Result = await userManager.AddToRoleAsync(user, Vm.SelectedRole);
                if (Result.Succeeded)
                    TempData["SuccessMessage"] = $"{Vm.SelectedRole} Assigned Successfully To {user.FristName}";
                return RedirectToAction(nameof(Index));

            }
        }
        [HttpGet]
        public async Task<IActionResult> RevokeRole(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var userRoles = await userManager.GetRolesAsync(user);

            var RevokeRoleVm = new AssignRoleViewModel
            {
                Id = id,
                Name = user.FristName + user.LastName,
                Email = user.Email,
                AvailableRoles = userRoles.Select(role => new SelectListItem
                {
                    Value = role,
                    Text = role
                }).ToList()
            };
            ViewBag.ActionType = "Revoke"; // or "Assign"
            return View(nameof(AssignRole), RevokeRoleVm);
        }

        [HttpPost]
        public async Task<IActionResult> RevokeRole(string id, AssignRoleViewModel Vm)
        {
            var user = await userManager.FindByIdAsync(id);

            if (!await userManager.IsInRoleAsync(user, Vm.SelectedRole))
            {
                TempData["ErrorMessage"] = $"{Vm.SelectedRole} Is Not Assigned To {user.FristName}";
                return RedirectToAction(nameof(Index));
            }

            var result = await userManager.RemoveFromRoleAsync(user, Vm.SelectedRole);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = $"{Vm.SelectedRole} Revoked Successfully From {user.FristName}";
            }
            await userManager.UpdateSecurityStampAsync(user);

            return RedirectToAction(nameof(Index));
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
