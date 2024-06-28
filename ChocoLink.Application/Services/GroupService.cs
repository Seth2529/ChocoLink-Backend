using ChocoLink.Domain.Entity;
using ChocoLink.Domain.IRepository;
using ChocoLink.Domain.IService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChocoLink.Application.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;

        public GroupService(IGroupRepository repository)
        {
            _groupRepository = repository;
        }

        public Group GetGroupName(string groupName)
        {
            return _groupRepository.GetGroupName(groupName);
        }

        public IEnumerable<Group> GetAllGroups()
        {
            return _groupRepository.GetAllGroups();
        }
        public IEnumerable<Group> GetGroupsByUserId(int userId)
        {
            return _groupRepository.GetGroupsByUserId(userId);
        }

        public void AddGroup(Group group)
        {
            if (GroupExist(group.GroupName))
            {
                throw new Exception("Nome do grupo já cadastrado.");
            }

            var id = _groupRepository.NextAvailableID();
            group.GroupID = id;
            _groupRepository.AddGroup(group);
        }

        public async Task<bool> DeleteGroup(int groupId)
        {
            try
            {
                var isDeleted = await _groupRepository.DeleteGroup(groupId);
                return isDeleted;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao deletar o grupo: {ex.Message}");
            }

            return false; 
        }

        public Group GetGroupById(int groupID)
        {
            return _groupRepository.GetGroupById(groupID);
        }

        public bool GroupExist(string groupName)
        {
            var group = _groupRepository.GetGroupName(groupName);
            return group != null;
        }

        public void UpdateGroup(Group group)
        {
            _groupRepository.UpdateGroup(group);
        }

        public void AddParticipant(GroupUser groupUser)
        {
            int nextGroupUserId = _groupRepository.NextAvailableGroupUserID();
            groupUser.GroupUserID = nextGroupUserId;
            _groupRepository.AddParticipant(groupUser);
        }

        public (int currentParticipants, int maxParticipants) GetParticipantCount(int groupId)
        {
            var group = _groupRepository.GetGroupById(groupId);
            var currentParticipants = _groupRepository.GetParticipantCount(groupId);
            var maxParticipants = group.MaxParticipants;

            return (currentParticipants, maxParticipants);
        }
    }
}
