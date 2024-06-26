﻿using ChocoLink.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocoLink.Domain.IService
{
    public interface IInviteService
    {
        public User GetUserByEmail(string email);
        void InviteUserToGroup(int groupId, string email);
        void AcceptInvitation(int invitationId);
        public Invite GetInvitationById(int invitationId);
        public void InviteOrRegisterUser(int groupId, string email);
        public (int currentParticipants, int maxParticipants) GetParticipantCount(int groupId);

    }
}
