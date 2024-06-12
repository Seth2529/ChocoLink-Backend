using System.ComponentModel.DataAnnotations;

namespace ChocoLink.API.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
