using System.ComponentModel.DataAnnotations;

namespace FilmZone.ViewModels.Identitiy
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Email Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
