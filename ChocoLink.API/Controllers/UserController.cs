using ChocoLink.API.ViewModels;
using ChocoLink.Data.EntityFramework;
using ChocoLink.Domain.Entity;
using ChocoLink.Domain.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChocoLink.API.Controllers
{
    [Route("User")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly Context db = new Context();
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(NewUserViewModel user)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    byte[] photoBytes;
                    using (var memoryStream = new MemoryStream())
                    {
                        await user.Photo.CopyToAsync(memoryStream);
                        photoBytes = memoryStream.ToArray();
                    }

                    User add = new()
                        {
                            Photo = photoBytes,
                            Username = user.Username,
                            Password = user.Password,
                            Email = user.Email
                        };
                        _userService.AddUser(add);
                        return Ok("Adicionado com Sucesso");
                    }
                return BadRequest("Dados inválidos.");
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromForm] AuthenticateViewModel authenticate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User user = _userService.GetUserByEmail(authenticate.Email);
                    if (user != null && user.PasswordValidate(authenticate.Password))
                    {
                        return Ok(new { success = true, message = "" });
                    }
                    else
                    {
                        return Unauthorized(new { success = false, message = "E-mail ou senha inválidos" });
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro no servidor: {ex.Message}" });
            }
        }
    }
}
