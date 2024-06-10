using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace ChocoLink.Domain.Entity
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[] Photo { get; set; }


        public ICollection<GroupUser> GroupUsers { get; set; } = new List<GroupUser>();

        public bool PasswordValidate(string password)
        {
            return Password == password;
        }
    }
}
