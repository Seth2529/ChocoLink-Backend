﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocoLink.Domain.Entity
{
    public class Invite
    {
        public int InviteId { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public DateTime InvitationDate { get; set; }
        public DateTime? ResponseDate { get; set; }

        public int GroupId { get; set; }
        public int UserID { get; set; }
        public virtual Group Group { get; set; }
        public virtual User User { get; set; }

    }
}
