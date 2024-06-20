using ChocoLink.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocoLink.Domain.IRepository
{
    public interface IGroupRepository
    {
        public Group GetGroupById(int GroupID);
        public IEnumerable<Group> GetAllGroups();
        public int NextAvailableID();
        public int NextAvailableGroupUserID();
        public void AddGroup(Group group);
        public Group GetGroupName(string Groupname);
        void UpdateGroup(Group group);
        public void AddParticipant(GroupUser groupUser);
        public int GetParticipantCount(int groupId);
        public void AddInvitation(Invite invite);
        public Invite GetInvitationById(int invitationId);
        public void UpdateInvitation(Invite invite);

    }
}
