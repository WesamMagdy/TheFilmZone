using FilmZone.ViewModels.Identitiy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using System.Threading.Tasks;

namespace FilmZone.Controllers
{
    public class AccountController : Controller
    {
        private AccountProvider AccountProvider;
        private UserManager<ApplicationUser> UserManager;
        private SignInManager<ApplicationUser> SignInManager;

        public AccountController(AccountProvider accountProvider, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            AccountProvider = accountProvider;
            UserManager = userManager;
            SignInManager = signInManager;
        }
        //Sign up
        //Sign in
        //Sign out
        [HttpGet]
        public IActionResult SignUp()
        {
            return View(new RegisterViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> SignUp(RegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = AccountProvider.ToUser(vm);
                var result = await UserManager.CreateAsync(user,vm.Password);
                if (result.Succeeded)
                   return RedirectToAction(nameof(SignIn));
                else
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);

            }
            return View(vm);
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> SignIn(LoginViewModel Vm)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(Vm.Email.ToLower());
                if(user != null)
                {
                  var passStatus = await UserManager.CheckPasswordAsync(user, Vm.Password);
                    if(passStatus)
                    {
                        var result = await SignInManager.PasswordSignInAsync(user, Vm.Password, Vm.RememberMe,false);
                        if (result.Succeeded)
                        {
                          return  RedirectToAction("index", "home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Wrong Password");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Incorrect Email");
                }
            }
            return View(Vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public new async Task<IActionResult> SignOut()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }

    }
}