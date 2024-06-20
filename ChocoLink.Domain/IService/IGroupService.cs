using ChocoLink.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocoLink.Domain.IService
{
    public interface IGroupService
    {
        public Group GetGroupById(int GroupID);
        public void AddGroup(Group group);
        public IEnumerable<Group> GetAllGroups();
        public Group GetGroupName(string groupName);
        public bool GroupExist(string groupName);
        void UpdateGroup(Group group);
        public void AddParticipant(GroupUser groupUser);
        public (int currentParticipants, int maxParticipants) GetParticipantCount(int groupId);
        public Invite GetInvitationById(int invitationId);

    }
}
