using ChocoLink.Domain.Entity;
using ChocoLink.Domain.Interfaces;
using ChocoLink.Domain.IRepository;
using ChocoLink.Domain.IService;
using ChocoLink.Infra.EmailService;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

public class InviteService : IInviteService
{
    private readonly IUserRepository _userRepository;
    private readonly IGroupRepository _groupRepository;
    private readonly IInviteRepository _inviteRepository;
    private readonly EmailConfig _emailConfig;

    public InviteService(IUserRepository userRepository, IInviteRepository inviteRepository, IGroupRepository groupRepository, IOptions<EmailConfig> emailConfig)
    {
        _userRepository = userRepository;
        _inviteRepository = inviteRepository;
        _groupRepository = groupRepository;
        _emailConfig = emailConfig.Value;
    }

    public User GetUserByEmail(string email)
    {
        return _userRepository.GetUserByEmail(email);
    }

    public void InviteUserToGroup(int groupId, string email)
    {
        var user = _userRepository.GetUserByEmail(email);
        int userId = user?.UserId ?? 0;

        if (user == null)
        {
            SendEmailInvite(email, groupId);
            return;
        }

        var (currentParticipants, maxParticipants) = GetParticipantCount(groupId);

        if (currentParticipants >= maxParticipants)
        {
            throw new Exception($"O grupo atingiu o número máximo de participantes ({maxParticipants}).");
        }

        var existingInvite = _inviteRepository.GetPendingInvite(groupId, userId);
        if (existingInvite != null)
        {
            throw new Exception("Já existe um convite deste grupo pendente/aceito para este usuário.");
        }

        int inviteId = _inviteRepository.NextAvailableID();
        var invitation = new Invite
        {
            InviteId = inviteId,
            GroupId = groupId,
            UserID = userId,
            Email = email,
            Status = "Pendente",
            InvitationDate = DateTime.Now,
        };

        _inviteRepository.AddInvitation(invitation);
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
            InviteUserToGroup(groupId, email);
        }
    }

    private void SendEmailInvite(string email, int groupId)
    {
        string subject = "Convite para o app ChocoLink";
        string body = $"Você foi convidado para se juntar a um grupo. Por favor, registre-se no nosso aplicativo ChocoLink.";
        Email.Enviar(subject, body, email, _emailConfig);
    }

    public void AcceptInvitation(int invitationId)
    {
        var invitation = _inviteRepository.GetInvitationById(invitationId);
        if (invitation == null || invitation.Status != "Pendente")
        {
            throw new Exception("Convite inválido");
        }

        var user = _userRepository.GetUserByEmail(invitation.Email);
        if (user == null)
        {
            throw new Exception("Usuário não encontrado");
        }

        int groupUsersId = _inviteRepository.NextAvailableID();
        var groupUser = new GroupUser
        {
            GroupUserID = groupUsersId,
            GroupID = invitation.GroupId,
            UserID = user.UserId,
            PerfilID = 2,
        };

        _groupRepository.AddParticipant(groupUser);

        invitation.Status = "Aceito";
        invitation.ResponseDate = DateTime.Now;
        _inviteRepository.UpdateInvitation(invitation);
    }

    public Invite GetInvitationById(int invitationId)
    {
        return _inviteRepository.GetInvitationById(invitationId);
    }
    public (int currentParticipants, int maxParticipants) GetParticipantCount(int groupId)
    {
        var group = _groupRepository.GetGroupById(groupId);
        var currentParticipants = _groupRepository.GetParticipantCount(groupId);
        var maxParticipants = group.MaxParticipants;

        return (currentParticipants, maxParticipants);
    }
}
