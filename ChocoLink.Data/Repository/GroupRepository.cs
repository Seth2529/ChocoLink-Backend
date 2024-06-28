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
        private readonly Context Context;

        public GroupRepository(Context context)
        {
            Context = context;
        }

        public IEnumerable<Group> GetAllGroups()
        {
            return Context.Groups.ToList();
        }
        public IEnumerable<Group> GetGroupsByUserId(int userId)
        {
            return Context.GroupUsers
                          .Where(gu => gu.UserID == userId)
                          .Include(gu => gu.Group)
                          .Select(gu => gu.Group)
                          .ToList();
        }

        public void AddGroup(Group group)
        {
            Context.Groups.Add(group);
            Context.SaveChanges();
        }

        public async Task<bool> DeleteGroup(int groupId)
        {
            try
            {
                var group = await Context.Groups.FindAsync(groupId);

                if (group == null)
                {
                    return false;
                }

                var groupUsers = Context.GroupUsers.Where(gu => gu.GroupID == groupId);
                Context.GroupUsers.RemoveRange(groupUsers);

                var invites = Context.Invites.Where(invite => invite.GroupId == groupId);
                Context.Invites.RemoveRange(invites);

                Context.Groups.Remove(group);

                await Context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao deletar o grupo: {ex.Message}");
            }
        }

        public Group GetGroupById(int groupID)
        {
            return Context.Groups.First(g => g.GroupID == groupID);
        }

        public int NextAvailableID()
        {
            var ExistId = Context.Groups.Select(g => g.GroupID).ToList();
            int NextID = 1;

            while (ExistId.Contains(NextID))
            {
                NextID++;
            }

            return NextID;
        }
        public int NextAvailableGroupUserID()
        { 
        var ExistId = Context.GroupUsers.Select(g => g.GroupUserID).ToList();
        int NextID = 1;

            while (ExistId.Contains(NextID))
            {
                NextID++;
            }

            return NextID;
        }
        public Group GetGroupName(string groupname)
        {
            return Context.Groups.FirstOrDefault(p => p.GroupName == groupname);
        }

        public void UpdateGroup(Group group)
        {
            Context.Groups.Update(group);
            Context.SaveChanges();
        }

        public void AddParticipant(GroupUser groupUser)
        {
            Context.GroupUsers.Add(groupUser);
            Context.SaveChanges();
        }

        public int GetParticipantCount(int groupId)
        {
            return Context.GroupUsers.Count(gu => gu.GroupID == groupId);
        }
    }
}
