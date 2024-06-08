using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChocoLink.Domain.Entity;
using ChocoLink.Domain.Interfaces;
using ChocoLink.Domain.IRepository;
using ChocoLink.Domain.IService;

namespace ChocoLink.Application.Services
{
    public class UserService : IUserService
    {

        IUserRepository _userRepository;
        public UserService(IUserRepository repository)
        {
            _userRepository = repository;
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

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
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
