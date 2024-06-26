using ChocoLink.API.ViewModels;
using ChocoLink.Data.EntityFramework;
using ChocoLink.Domain.Entity;
using ChocoLink.Domain.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChocoLink.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly Context db = new Context();
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("TestDbConnection")]
        public IActionResult TestDbConnection()
        {
            try
            {
                using (var context = new Context())
                {
                    if (context.CanConnect())
                    {
                        return Ok("Conexão com o banco de dados realizada com sucesso.");
                    }
                    else
                    {
                        return StatusCode(500, "Falha ao conectar com o banco de dados.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log detailed error information
                Console.WriteLine($"Erro ao testar conexão com o banco de dados: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                return StatusCode(500, $"Erro ao testar conexão com o banco de dados: {ex.Message}");
            }
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

        [HttpGet("GetUserById")]
        public IActionResult GetUserById([FromQuery] int userid)
        {
            try
            {
                var user = _userService.GetUserById(userid);
                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound("Usuário não encontrado.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro no servidor: {ex.Message}" });
            }
        }
    }
}
