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

        public InviteController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("InviteUser")]
        public IActionResult InviteUserToGroup([FromForm] InviteUserViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _userService.InviteUserToGroup(model.GroupId, model.Email);
                    return Ok($"Convite enviado para {model.Email} com sucesso.");
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
    }
}
