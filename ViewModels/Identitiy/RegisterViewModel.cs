using System.ComponentModel.DataAnnotations;

namespace FilmZone.ViewModels.Identitiy
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Frist Name Is Required")]
        public string FristName { get; set; }
        [Required(ErrorMessage = "Last Name Is Required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]

        public string Email { get; set; }
        [Required(ErrorMessage = "Password Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage ="Password doesn't match")]
        public string ConfirmPassword { get; set; }
        public bool RememberMe { get; set; }
    }
}
