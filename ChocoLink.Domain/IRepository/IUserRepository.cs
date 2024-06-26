using ChocoLink.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocoLink.Domain.Interfaces
{
    public interface IUserRepository
    {
        public User GetUserById(int userId);
        public User GetUserByEmail(string email);
        public void AddUser(User user);
        //void UpdateUser(User user);
        void UpdateUserPassword(User user, string newPassword);
        public int NextAvailableID();
        public bool TestDatabaseConnection();
    }
}
