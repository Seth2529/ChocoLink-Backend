using ChocoLink.API.ViewModels;
using ChocoLink.Application.Services;
using ChocoLink.Domain.IService;
using Microsoft.AspNetCore.Mvc;

namespace ChocoLink.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InviteController : ControllerBase
    {
        private readonly IInviteService _inviteService;

        public InviteController(IInviteService inviteService)
        {
            _inviteService = inviteService;
        }

        [HttpPost("InviteUser")]
        public IActionResult InviteUserToGroup([FromForm] InviteUserViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _inviteService.InviteUserToGroup(model.GroupId, model.Email);
                    return Ok("Convite enviado para o usuário com sucesso.");
                }
                return BadRequest("Dados inválidos.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AcceptInvitation")]
        public IActionResult AcceptInvitation(int invitationId)
        {
            try
            {
                _inviteService.AcceptInvitation(invitationId);
                return Ok("Convite aceito com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetInvitation/{invitationId}")]
        public IActionResult GetInvitationById(int invitationId)
        {
            try
            {
                var invitation = _inviteService.GetInvitationById(invitationId);
                if (invitation == null)
                {
                    return NotFound("Convite não encontrado.");
                }
                return Ok(invitation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
