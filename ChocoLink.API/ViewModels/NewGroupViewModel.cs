using System.ComponentModel.DataAnnotations;

namespace ChocoLink.API.ViewModels
{
    public class NewGroupViewModel
    {
        [Required]
        public IFormFile Photo { get; set; }
        [Required]
        public string GroupName { get; set; }
        [Required(ErrorMessage = "A quantidade de participantes deve ser maior que 1")]
        public int MaxParticipants { get; set; }
        [Required(ErrorMessage = "O valor deve ser maior que 0")]
        public decimal Value { get; set; }
        [Required]
        public DateTime? DateDiscover { get; set; }
        public string Description { get; set; }
    }
}
