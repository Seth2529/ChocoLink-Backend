using ChocoLink.Data.EntityFramework;
using ChocoLink.Domain.Entity;
using ChocoLink.Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocoLink.Data.Repository
{
    public class GroupRepository : IGroupRepository
    {
        Context Context { get; set; }
        public GroupRepository(Context context) { Context = context; }


        public IEnumerable<Group> GetAllGroups()
        {
            return Context.Group.ToList();
        }

        public void AddGroup(Group group)
        {
            Context.Group.Add(group);
            Context.SaveChanges();
        }

        public Group GetGroupById(int groupID)
        {
            return Context.Group.First(g => g.GroupID == groupID);
        }

        public int NextAvailableID()
        {
            var ExistId = Context.Group.Select(g => g.GroupID).ToList();
            int NextID = 1;

            while (ExistId.Contains(NextID))
            {
                NextID++;
            }

            return NextID;
        }

        public Group GetGroupName(string Groupname)
        {
            return Context.Group.FirstOrDefault(p => p.GroupName == Groupname);
        }
        public void UpdateGroup(Group group)
        {
            throw new NotImplementedException();
        }
    }
}
