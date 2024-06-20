using ChocoLink.API.ViewModels;
using ChocoLink.Domain.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ChocoLink.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InviteController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IGroupService _groupService;

        public InviteController(IUserService userService, IGroupService groupService)
        {
            _userService = userService;
            _groupService = groupService;   
        }

        [HttpPost("InviteUser")]
        public IActionResult InviteUserToGroup([FromForm] InviteUserViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _userService.InviteUserToGroup(model.GroupId, model.UserId);
                    return Ok($"Convite enviado para o usuário com ID {model.UserId} com sucesso.");
                }
                return BadRequest("Dados inválidos.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro no servidor: {ex.Message}" });
            }
        }

        [HttpPost("AcceptInvitation")]
        public IActionResult AcceptInvitation(int invitationId)
        {
            try
            {
                _userService.AcceptInvitation(invitationId);
                return Ok("Convite aceito com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro no servidor: {ex.Message}" });
            }
        }
        [HttpGet("GetInvitation/{invitationId}")]
        public IActionResult GetInvitationById(int invitationId)
        {
            try
            {
                var invitation = _groupService.GetInvitationById(invitationId);
                if (invitation == null)
                {
                    return NotFound("Convite não encontrado.");
                }
                return Ok(invitation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro no servidor: {ex.Message}" });
            }
        }
    }
}
