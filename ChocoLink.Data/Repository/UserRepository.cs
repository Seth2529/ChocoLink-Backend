using ChocoLink.Data.EntityFramework;
using ChocoLink.Domain.Entity;
using ChocoLink.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocoLink.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        Context Context { get; set; }
        public UserRepository(Context context) { Context = context; }

        public void AddUser(User user)
        {
            Context.User.Add(user);
            Context.SaveChanges();
        }

        public User GetUserById(int userId)
        {
            return Context.User.First(m => m.UserId == userId);
        }

        public int NextAvailableID()
        {
            var ExistId = Context.User.Select(p => p.UserId).ToList();
            int NextID = 1;

            while (ExistId.Contains(NextID))
            {
                NextID++;
            }

            return NextID;
        }
        public User GetUserByEmail(string email)
        {
            return Context.User.FirstOrDefault(p => p.Email == email);
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

    }
}
