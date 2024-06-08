using ChocoLink.Domain.Entity;
using ChocoLink.Domain.IRepository;
using ChocoLink.Domain.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ChocoLink.Application.Services
{
    public class GroupService : IGroupService
    {

        IGroupRepository _groupRepository;
        public GroupService(IGroupRepository repository)
        {
            _groupRepository = repository;
        }

        public Group GetGroupName(string Groupname)
        {
            return _groupRepository.GetGroupName(Groupname);
        }

        public IEnumerable<Group> GetAllGroups()
        {
            return _groupRepository.GetAllGroups();
        }

        public void AddGroup(Group group)
        {
            if (GroupExist(group.GroupName))
            {
                throw new Exception("Nome do grupo já cadastrado");
            }

            var id = _groupRepository.NextAvailableID();
            group.GroupID = id;
            _groupRepository.AddGroup(group);
        }

        public Group GetGroupById(int GroupID)
        {
            return _groupRepository.GetGroupById(GroupID);
        }

        public bool GroupExist(string groupName)
        {
            var pessoa = _groupRepository.GetGroupName(groupName);
            return pessoa != null;
        }

        public void UpdateGroup(Group group)
        {
            throw new NotImplementedException();
        }
    }
}
