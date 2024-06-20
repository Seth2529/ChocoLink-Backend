using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocoLink.Domain.Entity
{
    public class Group
    {
        public int GroupID { get; set; }
        public byte[] Photo { get; set; }  // Armazena a imagem como array de bytes
        public string GroupName { get; set; }
        public int MaxParticipants { get; set; }
        public decimal Value { get; set; }
        public DateTime? DateDiscover { get; set; }
        public string Description { get; set; }


        public ICollection<GroupUser> GroupUsers { get; set; } = new List<GroupUser>();
        public ICollection<Invite> Invitations { get; set; } = new List<Invite>();
    }
}
