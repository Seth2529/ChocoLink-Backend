using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChocoLink.Domain.Entity;
using ChocoLink.Domain.Interfaces;
using ChocoLink.Domain.IRepository;
using ChocoLink.Domain.IService;
using ChocoLink.Infra.EmailService;
using Microsoft.Extensions.Options;

namespace ChocoLink.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<User> Authenticate(string login, string password)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(int UserId)
        {
            return _userRepository.GetUserById(UserId);
        }

        public void AddUser(User user)
        {
            if (UserExist(user.Email))
            {
                throw new Exception("Email já cadastrado");
            }

            var id = _userRepository.NextAvailableID();
            user.UserId = id;
            _userRepository.AddUser(user);
        }
        public bool TestDatabaseConnection()
        {
            return _userRepository.TestDatabaseConnection();
        }
        //public void UpdateUser(User user)
        //{
        //    _userRepository.UpdateUser(user);
        //}
        public void UpdateUserPassword(User user, string newpassword)
        {
            _userRepository.UpdateUserPassword(user, newpassword);
        }

        public bool UserExist(string email)
        {
            var pessoa = _userRepository.GetUserByEmail(email);
            return pessoa != null;
        }

        public User GetUserByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email);
        }

    }
}