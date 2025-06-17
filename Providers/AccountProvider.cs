using FilmZone.ViewModels.Identitiy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FilmZone.Providers
{
    public class AccountProvider
    {
    
        private readonly UserManager<ApplicationUser> UserManager;
        public AccountProvider(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }
        public ApplicationUser ToUser(RegisterViewModel vm)
        {
            var user = new ApplicationUser()
            {
                UserName = vm.Email.ToLower(),
                Email = vm.Email.ToLower(),
                FristName = vm.FristName,
                LastName = vm.LastName,
                RememberMe = vm.RememberMe,
            };
            return user;
        }

    }   
}
