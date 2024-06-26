﻿using ChocoLink.Data.EntityFramework;
using ChocoLink.Domain.Entity;
using ChocoLink.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public bool TestDatabaseConnection()
        {
            return Context.CanConnect();
        }
        public void AddUser(User user)
        {
            Context.Users.Add(user);
            Context.SaveChanges();
        }

        public User GetUserById(int userId)
        {
            return Context.Users.First(m => m.UserId == userId);
        }

        public int NextAvailableID()
        {
            var ExistId = Context.Users.Select(p => p.UserId).ToList();
            int NextID = 1;

            while (ExistId.Contains(NextID))
            {
                NextID++;
            }

            return NextID;
        }
        public User GetUserByEmail(string email)
        {
            return Context.Users.FirstOrDefault(p => p.Email == email);
        }

        //public void UpdateUser(User user)
        //{
        //    var existingUser = Context.Users.FirstOrDefault(u => u.Email == user.Email);
        //    if (existingUser != null)
        //    {
        //        existingUser.Password = user.Password;
        //    }
        //}

        public void UpdateUserPassword(User user, string newPassword)
        {
            var existingUser = Context.Users.FirstOrDefault(u => u.Email == user.Email);
            if (existingUser != null)
            {
                existingUser.Password = newPassword;
                Context.SaveChanges();
            }
        }

    }
}
