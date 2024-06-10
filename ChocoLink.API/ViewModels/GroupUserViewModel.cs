using ChocoLink.Domain.Entity;

namespace ChocoLink.API.ViewModels
{
    public class GroupUserViewModel
    {
        public int GroupUserID { get; set; }
        public int GroupID { get; set; }
        public int UserID { get; set; }

        public Group Group { get; set; }
        public User User { get; set; }
    }
}
