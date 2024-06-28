using ChocoLink.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChocoLink.Domain.IRepository
{
    public interface IInviteRepository
    {
        public void AddInvitation(Invite invite);
        public List<Invite> GetInvitationByUserId(int userId);
        public Invite GetInvitationById(int invitationId);
        public void DeleteInvitation(int inviteId);
        public void UpdateInvitation(Invite invite);
        public int NextAvailableID();
        public Invite GetPendingInvite(int groupId, int userId);
    }
}
