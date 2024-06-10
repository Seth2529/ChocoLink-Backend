using ChocoLink.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace ChocoLink.API.ViewModels
{
    public class NewGroupUserViewModel
    {
        [Required]
        public int GroupID { get; set; }
        [Required]
        public int UserID { get; set; }

    }
}
