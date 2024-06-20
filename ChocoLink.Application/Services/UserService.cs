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

namespace ChocoLink.Application.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly EmailConfig _emailConfig;

        public UserService(IUserRepository userRepository, IGroupRepository groupRepository, EmailConfig emailConfig)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _emailConfig = emailConfig;
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

        //public void UpdateUser(User user)
        //{
        //    _userRepository.UpdateUser(user);
        //}
        public void UpdateUserPassword(User user, string newpassword)
        {
            _userRepository.UpdateUserPassword(user,newpassword);
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
        public void InviteOrRegisterUser(int groupId, string email)
        {
            var user = _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                SendEmailInvite(email, groupId);
            }
            else
            {
                InviteUserToGroup(groupId, user.Email);
            }
        }

        private void SendEmailInvite(string email, int groupId)
        {
            var invitation = new Invite
            {
                GroupId = groupId,
                Email = email,
                Status = "Pendente",
                InvitationDate = DateTime.UtcNow
            };

            _groupRepository.AddInvitation(invitation);
            string subject = "Convite para o Aplicativo e Grupo";
            string body = $"Você foi convidado para se juntar ao grupo com ID: {groupId}. Por favor, registre-se no nosso aplicativo.";
            Email.Enviar(subject, body, email, _emailConfig);
        }

        public void AcceptInvitation(int invitationId)
        {
            var invitation = _groupRepository.GetInvitationById(invitationId);
            if (invitation == null || invitation.Status != "Pendente")
            {
                throw new Exception("Convite inválido");
            }

            var user = _userRepository.GetUserByEmail(invitation.Email);
            if (user == null)
            {
                throw new Exception("Usuário não encontrado");
            }

            var groupUser = new GroupUser
            {
                GroupID = invitation.GroupId,
                UserID = user.UserId
            };

            _groupRepository.AddParticipant(groupUser);

            invitation.Status = "Aceito";
            invitation.ResponseDate = DateTime.UtcNow;
            _groupRepository.UpdateInvitation(invitation);
        }
    }
}