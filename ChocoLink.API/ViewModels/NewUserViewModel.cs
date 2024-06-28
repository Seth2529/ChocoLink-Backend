using System.ComponentModel.DataAnnotations;

namespace ChocoLink.API.ViewModels
{
    public class NewUserViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public IFormFile Photo { get; set; }
    }
}
