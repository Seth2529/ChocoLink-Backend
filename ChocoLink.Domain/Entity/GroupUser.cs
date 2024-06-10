using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocoLink.Domain.Entity
{
    public class GroupUser
    {
        public int GroupUserID { get; set; }
        public int GroupID { get; set; }
        public int UserID { get; set; }

        public Group Group { get; set; }
        public User User { get; set; }
    }
}