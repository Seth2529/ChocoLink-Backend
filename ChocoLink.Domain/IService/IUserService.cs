using ChocoLink.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocoLink.Domain.IService
{
    public interface IUserService
    {
        public Task<User> Authenticate(string login, string password);
        public User GetUserById(int UserId);
        public User GetUserByEmail(string email);
        public void AddUser(User user);
        //void UpdateUser(User user);
        void UpdateUserPassword(User user, string newPassword);
        public bool UserExist(string email);
        void InviteOrRegisterUser(int groupId, string email);
        void AcceptInvitation(int invitationId);
    }
}
