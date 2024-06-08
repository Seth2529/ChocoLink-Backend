namespace ChocoLink.API.ViewModels
{
    public class NewGroupViewModel
    {
        public IFormFile Photo { get; set; }
        public string GroupName { get; set; }
        public int NumberParticipants { get; set; }
        public decimal Value { get; set; }
        public DateTime? DateDiscover { get; set; }
        public string Description { get; set; }
    }
}
