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

        [HttpDelete("DeleteInvitation")]
        public IActionResult DeleteInvitation(int inviteId)
        {
            try
            {
                _inviteService.DeleteInvitation(inviteId);
                return Ok("Convite excluído com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetInvitation/{userId}")]
        public IActionResult GetInvitationByUserId(int userId)
        {
            try
            {
                var invitation = _inviteService.GetInvitationByUserId(userId);
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
