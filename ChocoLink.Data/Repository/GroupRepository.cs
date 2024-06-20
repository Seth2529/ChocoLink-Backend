using ChocoLink.Data.EntityFramework;
using ChocoLink.Domain.Entity;
using ChocoLink.Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChocoLink.Data.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly Context _context;

        public GroupRepository(Context context)
        {
            _context = context;
        }

        public IEnumerable<Group> GetAllGroups()
        {
            return _context.Groups.ToList();
        }

        public void AddGroup(Group group)
        {
            _context.Groups.Add(group);
            _context.SaveChanges();
        }

        public Group GetGroupById(int groupID)
        {
            return _context.Groups.First(g => g.GroupID == groupID);
        }

        public int NextAvailableID()
        {
            var ExistId = _context.Groups.Select(g => g.GroupID).ToList();
            int NextID = 1;

            while (ExistId.Contains(NextID))
            {
                NextID++;
            }

            return NextID;
        }
        public int NextAvailableGroupUserID()
        { 
        var ExistId = _context.GroupUsers.Select(g => g.GroupUserID).ToList();
        int NextID = 1;

            while (ExistId.Contains(NextID))
            {
                NextID++;
            }

            return NextID;
        }
        public Group GetGroupName(string groupname)
        {
            return _context.Groups.FirstOrDefault(p => p.GroupName == groupname);
        }

        public void UpdateGroup(Group group)
        {
            throw new NotImplementedException();
        }

        public void AddParticipant(GroupUser groupUser)
        {
            _context.GroupUsers.Add(groupUser);
            _context.SaveChanges();
        }

        public int GetParticipantCount(int groupId)
        {
            return _context.GroupUsers.Count(gu => gu.GroupID == groupId);
        }
        public void AddInvitation(Invite invite)
        {
            _context.Invites.Add(invite);
            _context.SaveChanges();
        }

        public void UpdateInvitation(Invite invite)
        {
            var existingInvite = _context.Invites.FirstOrDefault(i => i.InviteId == invite.InviteId);
            if (existingInvite != null)
            {
                existingInvite.Status = invite.Status;
                existingInvite.ResponseDate = invite.ResponseDate;
                _context.SaveChanges();
            }
        }
    }
}
