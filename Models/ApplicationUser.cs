using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FilmZone.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public string FristName { get; set; }
        [Required]

        public string LastName { get; set; }
        [Required]

        public bool RememberMe { get; set; }
    }
}
