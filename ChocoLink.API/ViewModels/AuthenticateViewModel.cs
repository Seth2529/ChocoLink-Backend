using System.ComponentModel.DataAnnotations;

namespace ChocoLink.API.ViewModels
{
    public class AuthenticateViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
