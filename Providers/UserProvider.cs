using FilmZone.ViewModels.Identitiy;
using Microsoft.AspNetCore.Identity;

namespace FilmZone.Providers
{
    public class UserProvider
    {
        private readonly UserManager<ApplicationUser> UserManager;
        public UserProvider(UserManager<ApplicationUser> userManager)
        {
            UserManager=userManager;
        }
        public async Task<UserViewModel>  ToUserView(ApplicationUser user)
        {
            var roles = await UserManager.GetRolesAsync(user);
            return new UserViewModel
            {

                Id = user.Id,
                Fname = user.FristName,
                Lname = user.LastName,
                Email = user.Email,
                Roles = roles
            };
        }
        public async Task<ApplicationUser> UpdateFromView(UserViewModel VM)
        {
            var user = await UserManager.FindByIdAsync(VM.Id);
                user.FristName = VM.Fname;
                user.LastName = VM.Lname;
                user.Email = VM.Email;
            return user;
        }
    }
}
