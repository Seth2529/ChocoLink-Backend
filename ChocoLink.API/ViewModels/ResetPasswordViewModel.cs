using System.ComponentModel.DataAnnotations;

namespace ChocoLink.API.ViewModels
{
    public class ResetPasswordViewModel
    {
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}
