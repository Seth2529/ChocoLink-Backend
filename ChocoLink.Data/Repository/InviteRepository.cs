using ChocoLink.Data.EntityFramework;
using ChocoLink.Domain.Entity;
using ChocoLink.Domain.IRepository;
using Microsoft.EntityFrameworkCore;

public class InviteRepository : IInviteRepository
{
    private readonly Context Context;

    public InviteRepository(Context context)
    {
        Context = context;
    }

    public void AddInvitation(Invite invite)
    {
        Context.Invites.Add(invite);
        Context.SaveChanges();
    }
    public void DeleteInvitation(int inviteId)
    {
        var invite = Context.Invites.FirstOrDefault(i => i.InviteId == inviteId);
        if (invite != null)
        {
            Context.Invites.Remove(invite);
            Context.SaveChanges();
        }
    }

    public List<Invite> GetInvitationByUserId(int userId)
    {
        return Context.Invites.Where(invite => invite.UserID == userId).ToList();
    }

    public void UpdateInvitation(Invite invite)
    {
        var existingInvite = Context.Invites.FirstOrDefault(i => i.InviteId == invite.InviteId);
        if (existingInvite != null)
        {
            existingInvite.Status = invite.Status;
            existingInvite.ResponseDate = invite.ResponseDate;
            Context.SaveChanges();
        }
    }

    public int NextAvailableID()
    {
        var existingIds = Context.Invites.Select(i => i.InviteId).ToList();
        int nextId = 1;

        while (existingIds.Contains(nextId))
        {
            nextId++;
        }

        return nextId;
    }
    public Invite GetInvitationById(int invitationId)
    {
        return Context.Invites.FirstOrDefault(i => i.InviteId == invitationId);
    }
    public Invite GetPendingInvite(int groupId, int userId)
    {
        return Context.Invites.FirstOrDefault(i => i.GroupId == groupId && i.UserID == userId);
    }
}
