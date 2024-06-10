using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocoLink.API.ViewModels
{
    public class GroupViewModel
    {
        public int GroupID { get; set; }
        public IFormFile Photo { get; set; }
        public string GroupName { get; set; }
        public int MaxParticipants { get; set; }
        public decimal Value { get; set; }
        public DateTime? DateDiscover { get; set; }
        public string Description { get; set; }
    }
}
