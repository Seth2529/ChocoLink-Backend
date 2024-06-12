using ChocoLink.API.ViewModels;
using ChocoLink.Domain.Entity;
using ChocoLink.Domain.IService;
using ChocoLink.Infra.EmailService;
using ChocoLink.Infra.ServiceToken;
using ChocoLink.Infra.ServiceToken.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ChocoLink.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly EmailConfig _emailConfig;

        public AuthController(ITokenService tokenService, IUserService userService, IOptions<EmailConfig> options)
        {
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _emailConfig = options?.Value ?? throw new ArgumentNullException(nameof(options));

            if (_emailConfig == null)
            {
                throw new ArgumentNullException(nameof(_emailConfig), "EmailConfig cannot be null");
            }
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromForm] ForgotPasswordViewModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Email))
                {
                    return BadRequest("E-mail é obrigatório.");
                }

                var user = _userService.GetUserByEmail(model.Email);
                if (user == null)
                {
                    return BadRequest("Usuário não encontrado.");
                }

                var tokenRequest = new TokenRequest { user = user.Email };
                var token = await _tokenService.GerarTokenJWT(tokenRequest);

                if (string.IsNullOrEmpty(token))
                {
                    return StatusCode(500, new { message = "Erro ao gerar o token." });
                }

                string emailContent = $"Seu token de redefinição de senha é: {token}";

                // Log para verificar o token enviado por e-mail
                Console.WriteLine($"Token Enviado: {token}");

                Email.Enviar("Redefinição de Senha", emailContent, user.Email, _emailConfig);

                return Ok("Se um usuário com esse e-mail existir, um token de redefinição de senha será enviado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro no servidor: {ex.Message}" });
            }
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordViewModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Token) || string.IsNullOrEmpty(model.NewPassword))
                {
                    return BadRequest("Token e nova senha são obrigatórios.");
                }

                var token = await _tokenService.DecodificarTokenJWT(model.Token);
                if (token == null || token.ValidTo < DateTime.UtcNow)
                {
                    Console.WriteLine("Token inválido ou expirado.");
                    return BadRequest("Token inválido ou expirado.");
                }

                var emailClaim = token.Claims.FirstOrDefault(c => c.Type == "usuario");
                if (emailClaim == null)
                {
                    Console.WriteLine("Token inválido. Claim 'usuario' não encontrada.");
                    return BadRequest("Token inválido.");
                }

                Console.WriteLine($"Token válido. Email claim: {emailClaim.Value}");

                var user = _userService.GetUserByEmail(emailClaim.Value);
                if (user == null)
                {
                    return BadRequest("Usuário não encontrado.");
                }

                // Atualiza a senha do usuário
                user.Password = model.NewPassword;  // Supondo que a senha esteja sendo atribuída diretamente
                _userService.UpdateUser(user);

                return Ok("Senha redefinida com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro no servidor: {ex.Message}" });
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
